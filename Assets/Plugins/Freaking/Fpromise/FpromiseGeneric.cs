using Freaking.FpromiseUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Freaking
{
    /// <summary>
    /// Implements a C# promise.
    /// https://developer.mozilla.org/en/docs/Web/JavaScript/Reference/Global_Objects/Fpromise
    /// </summary>
    public interface IFpromise<FpromisedT>
    {
        /// <summary>
        /// Set the name of the promise, useful for debugging.
        /// </summary>
        IFpromise<FpromisedT> WithName(string name);

        /// <summary>
        /// Completes the promise. 
        /// onResolved is called on successful completion.
        /// onRejected is called on error.
        /// </summary>
        void Done(Action<FpromisedT> onResolved, Action<Exception> onRejected);

        /// <summary>
        /// Completes the promise. 
        /// onResolved is called on successful completion.
        /// Adds a default error handler.
        /// </summary>
        void Done(Action<FpromisedT> onResolved);

        /// <summary>
        /// Complete the promise. Adds a default error handler.
        /// </summary>
        void Done();

        /// <summary>
        /// Handle errors for the promise. 
        /// </summary>
        IFpromise<FpromisedT> Catch(Action<Exception> onRejected);

        /// <summary>
        /// Add a resolved callback that chains a value promise (optionally converting to a different value type).
        /// </summary>
        IFpromise<ConvertedT> Then<ConvertedT>(Func<FpromisedT, IFpromise<ConvertedT>> onResolved);

        /// <summary>
        /// Add a resolved callback that chains a non-value promise.
        /// </summary>
        IFpromise Then(Func<FpromisedT, IFpromise> onResolved);

        /// <summary>
        /// Add a resolved callback.
        /// </summary>
        IFpromise<FpromisedT> Then(Action<FpromisedT> onResolved);

        /// <summary>
        /// Add a resolved callback and a rejected callback.
        /// The resolved callback chains a value promise (optionally converting to a different value type).
        /// </summary>
        IFpromise<ConvertedT> Then<ConvertedT>(Func<FpromisedT, IFpromise<ConvertedT>> onResolved, Action<Exception> onRejected);

        /// <summary>
        /// Add a resolved callback and a rejected callback.
        /// The resolved callback chains a non-value promise.
        /// </summary>
        IFpromise Then(Func<FpromisedT, IFpromise> onResolved, Action<Exception> onRejected);

        /// <summary>
        /// Add a resolved callback and a rejected callback.
        /// </summary>
        IFpromise<FpromisedT> Then(Action<FpromisedT> onResolved, Action<Exception> onRejected);

        /// <summary>
        /// Return a new promise with a different value.
        /// May also change the type of the value.
        /// </summary>
        IFpromise<ConvertedT> Transform<ConvertedT>(Func<FpromisedT, ConvertedT> transform);

        /// <summary>
        /// Chain an enumerable of promises, all of which must resolve.
        /// Returns a promise for a collection of the resolved results.
        /// The resulting promise is resolved when all of the promises have resolved.
        /// It is rejected as soon as any of the promises have been rejected.
        /// </summary>
        IFpromise<IEnumerable<ConvertedT>> ThenAll<ConvertedT>(Func<FpromisedT, IEnumerable<IFpromise<ConvertedT>>> chain);

        /// <summary>
        /// Chain an enumerable of promises, all of which must resolve.
        /// Converts to a non-value promise.
        /// The resulting promise is resolved when all of the promises have resolved.
        /// It is rejected as soon as any of the promises have been rejected.
        /// </summary>
        IFpromise ThenAll(Func<FpromisedT, IEnumerable<IFpromise>> chain);

        /// <summary>
        /// Takes a function that yields an enumerable of promises.
        /// Returns a promise that resolves when the first of the promises has resolved.
        /// Yields the value from the first promise that has resolved.
        /// </summary>
        IFpromise<ConvertedT> ThenRace<ConvertedT>(Func<FpromisedT, IEnumerable<IFpromise<ConvertedT>>> chain);

        /// <summary>
        /// Takes a function that yields an enumerable of promises.
        /// Converts to a non-value promise.
        /// Returns a promise that resolves when the first of the promises has resolved.
        /// Yields the value from the first promise that has resolved.
        /// </summary>
        IFpromise ThenRace(Func<FpromisedT, IEnumerable<IFpromise>> chain);
    }

    /// <summary>
    /// Interface for a promise that can be rejected.
    /// </summary>
    public interface IRejectable
    {
        /// <summary>
        /// Reject the promise with an exception.
        /// </summary>
        void Reject(Exception ex);
    }

    /// <summary>
    /// Interface for a promise that can be rejected or resolved.
    /// </summary>
    public interface IPendingFpromise<FpromisedT> : IRejectable
    {
        /// <summary>
        /// Resolve the promise with a particular value.
        /// </summary>
        void Resolve(FpromisedT value);
    }

    /// <summary>
    /// Specifies the state of a promise.
    /// </summary>
    public enum FpromiseState
    {
        // The promise is in-flight.
        Pending,
        // The promise has been rejected.
        Rejected,
        // The promise has been resolved.
        Resolved
    }

    /// <summary>
    /// Implements a C# promise.
    /// https://developer.mozilla.org/en/docs/Web/JavaScript/Reference/Global_Objects/Fpromise
    /// </summary>
    public class Fpromise<FpromisedT> : IFpromise<FpromisedT>, IPendingFpromise<FpromisedT>, IFpromiseInfo
    {
        /// <summary>
        /// The exception when the promise is rejected.
        /// </summary>
        private Exception rejectionException;

        /// <summary>
        /// The value when the promises is resolved.
        /// </summary>
        private FpromisedT resolveValue;

        /// <summary>
        /// Error handler.
        /// </summary>
        private List<RejectHandler> rejectHandlers;

        /// <summary>
        /// Completed handlers that accept a value.
        /// </summary>
        private List<Action<FpromisedT>> resolveCallbacks;
        private List<IRejectable> resolveRejectables;

        /// <summary>
        /// ID of the promise, useful for debugging.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Name of the promise, when set, useful for debugging.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Tracks the current state of the promise.
        /// </summary>
        public FpromiseState CurState { get; private set; }

        public Fpromise()
        {
            this.CurState = FpromiseState.Pending;
            this.Id = ++Fpromise.nextFpromiseId;

            if (Fpromise.EnableFpromiseTracking)
            {
                Fpromise.pendingFpromises.Add(this);
            }
        }

        public Fpromise(Action<Action<FpromisedT>, Action<Exception>> resolver)
        {
            this.CurState = FpromiseState.Pending;
            this.Id = ++Fpromise.nextFpromiseId;

            if (Fpromise.EnableFpromiseTracking)
            {
                Fpromise.pendingFpromises.Add(this);
            }

            try
            {
                resolver(
                    // Resolve
                    value => Resolve(value),

                    // Reject
                    ex => Reject(ex)
                );
            }
            catch (Exception ex)
            {
                Reject(ex);
            }
        }

        /// <summary>
        /// Add a rejection handler for this promise.
        /// </summary>
        private void AddRejectHandler(Action<Exception> onRejected, IRejectable rejectable)
        {
            if (rejectHandlers == null)
            {
                rejectHandlers = new List<RejectHandler>();
            }

            rejectHandlers.Add(new RejectHandler() { callback = onRejected, rejectable = rejectable });
            ;
        }

        /// <summary>
        /// Add a resolve handler for this promise.
        /// </summary>
        private void AddResolveHandler(Action<FpromisedT> onResolved, IRejectable rejectable)
        {
            if (resolveCallbacks == null)
            {
                resolveCallbacks = new List<Action<FpromisedT>>();
            }

            if (resolveRejectables == null)
            {
                resolveRejectables = new List<IRejectable>();
            }

            resolveCallbacks.Add(onResolved);
            resolveRejectables.Add(rejectable);
        }

        /// <summary>
        /// Invoke a single handler.
        /// </summary>
        private void InvokeHandler<T>(Action<T> callback, IRejectable rejectable, T value)
        {
            //            Argument.NotNull(() => callback);
            //            Argument.NotNull(() => rejectable);            

            try
            {
                callback(value);
            }
            catch (Exception ex)
            {
                rejectable.Reject(ex);
            }
        }

        /// <summary>
        /// Helper function clear out all handlers after resolution or rejection.
        /// </summary>
        private void ClearHandlers()
        {
            rejectHandlers = null;
            resolveCallbacks = null;
            resolveRejectables = null;
        }

        /// <summary>
        /// Invoke all reject handlers.
        /// </summary>
        private void InvokeRejectHandlers(Exception ex)
        {
            //            Argument.NotNull(() => ex);

            if (rejectHandlers != null)
            {
                rejectHandlers.Each(handler => InvokeHandler(handler.callback, handler.rejectable, ex));
            }

            ClearHandlers();
        }

        /// <summary>
        /// Invoke all resolve handlers.
        /// </summary>
        private void InvokeResolveHandlers(FpromisedT value)
        {
            if (resolveCallbacks != null)
            {
                for (int i = 0, maxI = resolveCallbacks.Count; i < maxI; i++)
                {
                    InvokeHandler(resolveCallbacks[i], resolveRejectables[i], value);
                }
            }

            ClearHandlers();
        }

        /// <summary>
        /// Reject the promise with an exception.
        /// </summary>
        public void Reject(Exception ex)
        {
            //            Argument.NotNull(() => ex);

            if (CurState != FpromiseState.Pending)
            {
                throw new ApplicationException("Attempt to reject a promise that is already in state: " + CurState + ", a promise can only be rejected when it is still in state: " + FpromiseState.Pending);
            }

            rejectionException = ex;
            CurState = FpromiseState.Rejected;

            if (Fpromise.EnableFpromiseTracking)
            {
                Fpromise.pendingFpromises.Remove(this);
            }

            InvokeRejectHandlers(ex);
        }

        /// <summary>
        /// Resolve the promise with a particular value.
        /// </summary>
        public void Resolve(FpromisedT value)
        {
            if (CurState != FpromiseState.Pending)
            {
                throw new ApplicationException("Attempt to resolve a promise that is already in state: " + CurState + ", a promise can only be resolved when it is still in state: " + FpromiseState.Pending);
            }

            resolveValue = value;
            CurState = FpromiseState.Resolved;

            if (Fpromise.EnableFpromiseTracking)
            {
                Fpromise.pendingFpromises.Remove(this);
            }

            InvokeResolveHandlers(value);
        }

        /// <summary>
        /// Completes the promise. 
        /// onResolved is called on successful completion.
        /// onRejected is called on error.
        /// </summary>
        public void Done(Action<FpromisedT> onResolved, Action<Exception> onRejected)
        {
            //            Argument.NotNull(() => onResolved);
            //            Argument.NotNull(() => onRejected);

            var resultFpromise = new Fpromise<FpromisedT>();
            resultFpromise.WithName(Name);

            ActionHandlers(resultFpromise, onResolved, onRejected);
        }

        /// <summary>
        /// Completes the promise. 
        /// onResolved is called on successful completion.
        /// Adds a default error handler.
        /// </summary>
        public void Done(Action<FpromisedT> onResolved)
        {
            //            Argument.NotNull(() => onResolved);

            var resultFpromise = new Fpromise<FpromisedT>();
            resultFpromise.WithName(Name);

            ActionHandlers(resultFpromise,
                onResolved,
                ex => Fpromise.PropagateUnhandledException(this, ex)
            );
        }

        /// <summary>
        /// Complete the promise. Adds a default error handler.
        /// </summary>
        public void Done()
        {
            var resultFpromise = new Fpromise<FpromisedT>();
            resultFpromise.WithName(Name);

            ActionHandlers(resultFpromise,
                value =>
                {
                },
                ex => Fpromise.PropagateUnhandledException(this, ex)
            );
        }

        /// <summary>
        /// Set the name of the promise, useful for debugging.
        /// </summary>
        public IFpromise<FpromisedT> WithName(string name)
        {
            this.Name = name;
            return this;
        }

        /// <summary>
        /// Handle errors for the promise. 
        /// </summary>
        public IFpromise<FpromisedT> Catch(Action<Exception> onRejected)
        {
            //            Argument.NotNull(() => onRejected);

            var resultFpromise = new Fpromise<FpromisedT>();
            resultFpromise.WithName(Name);

            Action<FpromisedT> resolveHandler = v =>
            {
                resultFpromise.Resolve(v);
            };

            Action<Exception> rejectHandler = ex =>
            {
                onRejected(ex);

                resultFpromise.Reject(ex);
            };

            ActionHandlers(resultFpromise, resolveHandler, rejectHandler);

            return resultFpromise;
        }

        /// <summary>
        /// Add a resolved callback that chains a value promise (optionally converting to a different value type).
        /// </summary>
        public IFpromise<ConvertedT> Then<ConvertedT>(Func<FpromisedT, IFpromise<ConvertedT>> onResolved)
        {
            return Then(onResolved, null);
        }

        /// <summary>
        /// Add a resolved callback that chains a non-value promise.
        /// </summary>
        public IFpromise Then(Func<FpromisedT, IFpromise> onResolved)
        {
            return Then(onResolved, null);
        }

        /// <summary>
        /// Add a resolved callback.
        /// </summary>
        public IFpromise<FpromisedT> Then(Action<FpromisedT> onResolved)
        {
            return Then(onResolved, null);
        }

        /// <summary>
        /// Add a resolved callback and a rejected callback.
        /// The resolved callback chains a value promise (optionally converting to a different value type).
        /// </summary>
        public IFpromise<ConvertedT> Then<ConvertedT>(Func<FpromisedT, IFpromise<ConvertedT>> onResolved, Action<Exception> onRejected)
        {
            // This version of the function must supply an onResolved.
            // Otherwise there is now way to get the converted value to pass to the resulting promise.
            //            Argument.NotNull(() => onResolved); 

            var resultFpromise = new Fpromise<ConvertedT>();
            resultFpromise.WithName(Name);

            Action<FpromisedT> resolveHandler = v =>
            {
                onResolved(v)
                    .Then(
                    // Should not be necessary to specify the arg type on the next line, but Unity (mono) has an internal compiler error otherwise.
                    (ConvertedT chainedValue) => resultFpromise.Resolve(chainedValue),
                    ex => resultFpromise.Reject(ex)
                );
            };

            Action<Exception> rejectHandler = ex =>
            {
                if (onRejected != null)
                {
                    onRejected(ex);
                }

                resultFpromise.Reject(ex);
            };

            ActionHandlers(resultFpromise, resolveHandler, rejectHandler);

            return resultFpromise;
        }

        /// <summary>
        /// Add a resolved callback and a rejected callback.
        /// The resolved callback chains a non-value promise.
        /// </summary>
        public IFpromise Then(Func<FpromisedT, IFpromise> onResolved, Action<Exception> onRejected)
        {
            var resultFpromise = new Fpromise();
            resultFpromise.WithName(Name);

            Action<FpromisedT> resolveHandler = v =>
            {
                if (onResolved != null)
                {
                    onResolved(v)
                        .Then(
                        () => resultFpromise.Resolve(),
                        ex => resultFpromise.Reject(ex)
                    );
                }
                else
                {
                    resultFpromise.Resolve();
                }
            };

            Action<Exception> rejectHandler = ex =>
            {
                if (onRejected != null)
                {
                    onRejected(ex);
                }

                resultFpromise.Reject(ex);
            };

            ActionHandlers(resultFpromise, resolveHandler, rejectHandler);

            return resultFpromise;
        }

        /// <summary>
        /// Add a resolved callback and a rejected callback.
        /// </summary>
        public IFpromise<FpromisedT> Then(Action<FpromisedT> onResolved, Action<Exception> onRejected)
        {
            var resultFpromise = new Fpromise<FpromisedT>();
            resultFpromise.WithName(Name);

            Action<FpromisedT> resolveHandler = v =>
            {
                if (onResolved != null)
                {
                    onResolved(v);
                }

                resultFpromise.Resolve(v);
            };

            Action<Exception> rejectHandler = ex =>
            {
                if (onRejected != null)
                {
                    onRejected(ex);
                }

                resultFpromise.Reject(ex);
            };

            ActionHandlers(resultFpromise, resolveHandler, rejectHandler);

            return resultFpromise;
        }

        /// <summary>
        /// Return a new promise with a different value.
        /// May also change the type of the value.
        /// </summary>
        public IFpromise<ConvertedT> Transform<ConvertedT>(Func<FpromisedT, ConvertedT> transform)
        {
            //            Argument.NotNull(() => transform);

            var resultFpromise = new Fpromise<ConvertedT>();
            resultFpromise.WithName(Name);

            Action<FpromisedT> resolveHandler = v =>
            {
                resultFpromise.Resolve(transform(v));
            };

            Action<Exception> rejectHandler = ex =>
            {
                resultFpromise.Reject(ex);
            };

            ActionHandlers(resultFpromise, resolveHandler, rejectHandler);

            return resultFpromise;
        }

        /// <summary>
        /// Helper function to invoke or register resolve/reject handlers.
        /// </summary>
        private void ActionHandlers(IRejectable resultFpromise, Action<FpromisedT> resolveHandler, Action<Exception> rejectHandler)
        {
            if (CurState == FpromiseState.Resolved)
            {
                InvokeHandler(resolveHandler, resultFpromise, resolveValue);
            }
            else if (CurState == FpromiseState.Rejected)
            {
                InvokeHandler(rejectHandler, resultFpromise, rejectionException);
            }
            else
            {
                AddResolveHandler(resolveHandler, resultFpromise);
                AddRejectHandler(rejectHandler, resultFpromise);
            }
        }

        /// <summary>
        /// Chain an enumerable of promises, all of which must resolve.
        /// Returns a promise for a collection of the resolved results.
        /// The resulting promise is resolved when all of the promises have resolved.
        /// It is rejected as soon as any of the promises have been rejected.
        /// </summary>
        public IFpromise<IEnumerable<ConvertedT>> ThenAll<ConvertedT>(Func<FpromisedT, IEnumerable<IFpromise<ConvertedT>>> chain)
        {
            return Then(value => Fpromise<ConvertedT>.All(chain(value)));
        }

        /// <summary>
        /// Chain an enumerable of promises, all of which must resolve.
        /// Converts to a non-value promise.
        /// The resulting promise is resolved when all of the promises have resolved.
        /// It is rejected as soon as any of the promises have been rejected.
        /// </summary>
        public IFpromise ThenAll(Func<FpromisedT, IEnumerable<IFpromise>> chain)
        {
            return Then(value => Fpromise.All(chain(value)));
        }

        /// <summary>
        /// Returns a promise that resolves when all of the promises in the enumerable argument have resolved.
        /// Returns a promise of a collection of the resolved results.
        /// </summary>
        public static IFpromise<IEnumerable<FpromisedT>> All(params IFpromise<FpromisedT>[] promises)
        {
            return All((IEnumerable<IFpromise<FpromisedT>>)promises); // Cast is required to force use of the other All function.
        }

        /// <summary>
        /// Returns a promise that resolves when all of the promises in the enumerable argument have resolved.
        /// Returns a promise of a collection of the resolved results.
        /// </summary>
        public static IFpromise<IEnumerable<FpromisedT>> All(IEnumerable<IFpromise<FpromisedT>> promises)
        {
            var promisesArray = promises.ToArray();
            if (promisesArray.Length == 0)
            {
                return Fpromise<IEnumerable<FpromisedT>>.Resolved(EnumerableExtensions.Empty<FpromisedT>());
            }

            var remainingCount = promisesArray.Length;
            var results = new FpromisedT[remainingCount];
            var resultFpromise = new Fpromise<IEnumerable<FpromisedT>>();
            resultFpromise.WithName("All");

            promisesArray.Each((promise, index) =>
            {
                promise
                .Catch(ex =>
                {
                    if (resultFpromise.CurState == FpromiseState.Pending)
                    {
                            // If a promise errorred and the result promise is still pending, reject it.
                            resultFpromise.Reject(ex);
                    }
                })
                .Then(result =>
                {
                    results[index] = result;

                    --remainingCount;
                    if (remainingCount <= 0)
                    {
                            // This will never happen if any of the promises errorred.
                            resultFpromise.Resolve(results);
                    }
                })
                .Done();
            });

            return resultFpromise;
        }

        /// <summary>
        /// Takes a function that yields an enumerable of promises.
        /// Returns a promise that resolves when the first of the promises has resolved.
        /// Yields the value from the first promise that has resolved.
        /// </summary>
        public IFpromise<ConvertedT> ThenRace<ConvertedT>(Func<FpromisedT, IEnumerable<IFpromise<ConvertedT>>> chain)
        {
            return Then(value => Fpromise<ConvertedT>.Race(chain(value)));
        }

        /// <summary>
        /// Takes a function that yields an enumerable of promises.
        /// Converts to a non-value promise.
        /// Returns a promise that resolves when the first of the promises has resolved.
        /// Yields the value from the first promise that has resolved.
        /// </summary>
        public IFpromise ThenRace(Func<FpromisedT, IEnumerable<IFpromise>> chain)
        {
            return Then(value => Fpromise.Race(chain(value)));
        }

        /// <summary>
        /// Returns a promise that resolves when the first of the promises in the enumerable argument have resolved.
        /// Returns the value from the first promise that has resolved.
        /// </summary>
        public static IFpromise<FpromisedT> Race(params IFpromise<FpromisedT>[] promises)
        {
            return Race((IEnumerable<IFpromise<FpromisedT>>)promises); // Cast is required to force use of the other function.
        }

        /// <summary>
        /// Returns a promise that resolves when the first of the promises in the enumerable argument have resolved.
        /// Returns the value from the first promise that has resolved.
        /// </summary>
        public static IFpromise<FpromisedT> Race(IEnumerable<IFpromise<FpromisedT>> promises)
        {
            var promisesArray = promises.ToArray();
            if (promisesArray.Length == 0)
            {
                throw new ApplicationException("At least 1 input promise must be provided for Race");
            }

            var resultFpromise = new Fpromise<FpromisedT>();
            resultFpromise.WithName("Race");

            promisesArray.Each((promise, index) =>
            {
                promise
                .Catch(ex =>
                {
                    if (resultFpromise.CurState == FpromiseState.Pending)
                    {
                            // If a promise errorred and the result promise is still pending, reject it.
                            resultFpromise.Reject(ex);
                    }
                })
                .Then(result =>
                {
                    if (resultFpromise.CurState == FpromiseState.Pending)
                    {
                        resultFpromise.Resolve(result);
                    }
                })
                .Done();
            });

            return resultFpromise;
        }

        /// <summary>
        /// Convert a simple value directly into a resolved promise.
        /// </summary>
        public static IFpromise<FpromisedT> Resolved(FpromisedT promisedValue)
        {
            var promise = new Fpromise<FpromisedT>();
            promise.Resolve(promisedValue);
            return promise;
        }

        /// <summary>
        /// Convert an exception directly into a rejected promise.
        /// </summary>
        public static IFpromise<FpromisedT> Rejected(Exception ex)
        {
            //            Argument.NotNull(() => ex);

            var promise = new Fpromise<FpromisedT>();
            promise.Reject(ex);
            return promise;
        }
    }
}
