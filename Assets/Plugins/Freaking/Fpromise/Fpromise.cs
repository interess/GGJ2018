using System;
using System.Collections.Generic;
using System.Linq;
using Freaking.FpromiseUtils;

namespace Freaking
{
    #region Interfaces
    /// <summary>
    /// Implements a non-generic C# promise, this is a promise that simply resolves without delivering a value.
    /// https://developer.mozilla.org/en/docs/Web/JavaScript/Reference/Global_Objects/Fpromise
    /// </summary>
    public interface IFpromise
    {
        /// <summary>
        /// Set the name of the promise, useful for debugging.
        /// </summary>
        IFpromise WithName(string name);

        /// <summary>
        /// Completes the promise. 
        /// onResolved is called on successful completion.
        /// onRejected is called on error.
        /// </summary>
        void Done(Action onResolved, Action<Exception> onRejected);

        /// <summary>
        /// Completes the promise. 
        /// onResolved is called on successful completion.
        /// Adds a default error handler.
        /// </summary>
        void Done(Action onResolved);

        /// <summary>
        /// Complete the promise. Adds a default error handler.
        /// </summary>
        void Done();

        /// <summary>
        /// Handle errors for the promise. 
        /// </summary>
        IFpromise Catch(Action<Exception> onRejected);

        /// <summary>
        /// Add a resolved callback that chains a value promise (optionally converting to a different value type).
        /// </summary>
        IFpromise<ConvertedT> Then<ConvertedT>(Func<IFpromise<ConvertedT>> onResolved);

        /// <summary>
        /// Add a resolved callback that chains a non-value promise.
        /// </summary>
        IFpromise Then(Func<IFpromise> onResolved);

        /// <summary>
        /// Add a resolved callback.
        /// </summary>
        IFpromise Then(Action onResolved);

        /// <summary>
        /// Add a resolved callback and a rejected callback.
        /// The resolved callback chains a value promise (optionally converting to a different value type).
        /// </summary>
        IFpromise<ConvertedT> Then<ConvertedT>(Func<IFpromise<ConvertedT>> onResolved, Action<Exception> onRejected);

        /// <summary>
        /// Add a resolved callback and a rejected callback.
        /// The resolved callback chains a non-value promise.
        /// </summary>
        IFpromise Then(Func<IFpromise> onResolved, Action<Exception> onRejected);

        /// <summary>
        /// Add a resolved callback and a rejected callback.
        /// </summary>
        IFpromise Then(Action onResolved, Action<Exception> onRejected);

        /// <summary>
        /// Chain an enumerable of promises, all of which must resolve.
        /// The resulting promise is resolved when all of the promises have resolved.
        /// It is rejected as soon as any of the promises have been rejected.
        /// </summary>
        IFpromise ThenAll(Func<IEnumerable<IFpromise>> chain);

        /// <summary>
        /// Chain an enumerable of promises, all of which must resolve.
        /// Converts to a non-value promise.
        /// The resulting promise is resolved when all of the promises have resolved.
        /// It is rejected as soon as any of the promises have been rejected.
        /// </summary>
        IFpromise<IEnumerable<ConvertedT>> ThenAll<ConvertedT>(Func<IEnumerable<IFpromise<ConvertedT>>> chain);

        /// <summary>
        /// Chain a sequence of operations using promises.
        /// Reutrn a collection of functions each of which starts an async operation and yields a promise.
        /// Each function will be called and each promise resolved in turn.
        /// The resulting promise is resolved after each promise is resolved in sequence.
        /// </summary>
        IFpromise ThenSequence(Func<IEnumerable<Func<IFpromise>>> chain);

        /// <summary>
        /// Takes a function that yields an enumerable of promises.
        /// Returns a promise that resolves when the first of the promises has resolved.
        /// </summary>
        IFpromise ThenRace(Func<IEnumerable<IFpromise>> chain);

        /// <summary>
        /// Takes a function that yields an enumerable of promises.
        /// Converts to a value promise.
        /// Returns a promise that resolves when the first of the promises has resolved.
        /// </summary>
        IFpromise<ConvertedT> ThenRace<ConvertedT>(Func<IEnumerable<IFpromise<ConvertedT>>> chain);
    }

    /// <summary>
    /// Interface for a promise that can be rejected or resolved.
    /// </summary>
    public interface IPendingFpromise : IRejectable
    {
        /// <summary>
        /// Resolve the promise with a particular value.
        /// </summary>
        void Resolve();
    }

    /// <summary>
    /// Used to list information of pending promises.
    /// </summary>
    public interface IFpromiseInfo
    {
        /// <summary>
        /// Id of the promise.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Human-readable name for the promise.
        /// </summary>
        string Name { get; }
    }
    #endregion

    /// <summary>
    /// Implements a non-generic C# promise, this is a promise that simply resolves without delivering a value.
    /// https://developer.mozilla.org/en/docs/Web/JavaScript/Reference/Global_Objects/Fpromise
    /// </summary>
    public partial class Fpromise : IFpromise, IPendingFpromise, IFpromiseInfo
    {
        /// <summary>
        /// Set to true to enable tracking of promises.
        /// </summary>
        public static bool EnableFpromiseTracking = false;

        /// <summary>
        /// Event raised for unhandled errors.
        /// For this to work you have to complete your promises with a call to Done().
        /// </summary>
        public static event EventHandler<ExceptionEventArgs> UnhandledException
        {
            add { unhandlerException += value; }
            remove { unhandlerException -= value; }
        }
        private static EventHandler<ExceptionEventArgs> unhandlerException;

        /// <summary>
        /// Id for the next promise that is created.
        /// </summary>
        internal static int nextFpromiseId = 0;

        /// <summary>
        /// Information about pending promises.
        /// </summary>
        internal static HashSet<IFpromiseInfo> pendingFpromises = new HashSet<IFpromiseInfo>();

        /// <summary>
        /// Information about pending promises, useful for debugging.
        /// This is only populated when 'EnableFpromiseTracking' is set to true.
        /// </summary>
        public static IEnumerable<IFpromiseInfo> GetPendingFpromises()
        {
            return pendingFpromises;
        }

        /// <summary>
        /// The exception when the promise is rejected.
        /// </summary>
        private Exception rejectionException;

        /// <summary>
        /// Error handlers.
        /// </summary>
        private List<RejectHandler> rejectHandlers;

        /// <summary>
        /// Represents a handler invoked when the promise is resolved.
        /// </summary>
        public struct ResolveHandler
        {
            /// <summary>
            /// Callback fn.
            /// </summary>
            public Action callback;

            /// <summary>
            /// The promise that is rejected when there is an error while invoking the handler.
            /// </summary>
            public IRejectable rejectable;
        }

        /// <summary>
        /// Completed handlers that accept no value.
        /// </summary>
        private List<ResolveHandler> resolveHandlers;

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
            if (EnableFpromiseTracking)
            {
                pendingFpromises.Add(this);
            }
        }

        public Fpromise(Action<Action, Action<Exception>> resolver)
        {
            this.CurState = FpromiseState.Pending;
            if (EnableFpromiseTracking)
            {
                pendingFpromises.Add(this);
            }

            try
            {
                resolver(
                    // Resolve
                    () => Resolve(),

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

            rejectHandlers.Add(new RejectHandler()
            {
                callback = onRejected,
                rejectable = rejectable
            });
        }

        /// <summary>
        /// Add a resolve handler for this promise.
        /// </summary>
        private void AddResolveHandler(Action onResolved, IRejectable rejectable)
        {
            if (resolveHandlers == null)
            {
                resolveHandlers = new List<ResolveHandler>();
            }

            resolveHandlers.Add(new ResolveHandler()
            {
                callback = onResolved,
                rejectable = rejectable
            });
        }

        /// <summary>
        /// Invoke a single error handler.
        /// </summary>
        private void InvokeRejectHandler(Action<Exception> callback, IRejectable rejectable, Exception value)
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
        /// Invoke a single resolve handler.
        /// </summary>
        private void InvokeResolveHandler(Action callback, IRejectable rejectable)
        {
            //            Argument.NotNull(() => callback);
            //            Argument.NotNull(() => rejectable);

            try
            {
                callback();
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
            resolveHandlers = null;
        }

        /// <summary>
        /// Invoke all reject handlers.
        /// </summary>
        private void InvokeRejectHandlers(Exception ex)
        {
            //            Argument.NotNull(() => ex);

            if (rejectHandlers != null)
            {
                rejectHandlers.Each(handler => InvokeRejectHandler(handler.callback, handler.rejectable, ex));
            }

            ClearHandlers();
        }

        /// <summary>
        /// Invoke all resolve handlers.
        /// </summary>
        private void InvokeResolveHandlers()
        {
            if (resolveHandlers != null)
            {
                resolveHandlers.Each(handler => InvokeResolveHandler(handler.callback, handler.rejectable));
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

            if (EnableFpromiseTracking)
            {
                pendingFpromises.Remove(this);
            }

            InvokeRejectHandlers(ex);
        }


        /// <summary>
        /// Resolve the promise with a particular value.
        /// </summary>
        public void Resolve()
        {
            if (CurState != FpromiseState.Pending)
            {
                throw new ApplicationException("Attempt to resolve a promise that is already in state: " + CurState + ", a promise can only be resolved when it is still in state: " + FpromiseState.Pending);
            }

            CurState = FpromiseState.Resolved;

            if (EnableFpromiseTracking)
            {
                pendingFpromises.Remove(this);
            }

            InvokeResolveHandlers();
        }

        /// <summary>
        /// Completes the promise. 
        /// onResolved is called on successful completion.
        /// onRejected is called on error.
        /// </summary>
        public void Done(Action onResolved, Action<Exception> onRejected)
        {
            //            Argument.NotNull(() => onResolved);
            //            Argument.NotNull(() => onRejected);

            var resultFpromise = new Fpromise();
            resultFpromise.WithName(Name);

            ActionHandlers(resultFpromise, onResolved, onRejected);
        }

        /// <summary>
        /// Completes the promise. 
        /// onResolved is called on successful completion.
        /// Adds a default error handler.
        /// </summary>
        public void Done(Action onResolved)
        {
            //            Argument.NotNull(() => onResolved);

            var resultFpromise = new Fpromise();
            resultFpromise.WithName(Name);

            ActionHandlers(resultFpromise,
                onResolved,
                ex => Fpromise.PropagateUnhandledException(this, ex)
            );
        }

        /// <summary>
        /// Complete the promise. Adds a defualt error handler.
        /// </summary>
        public void Done()
        {
            var resultFpromise = new Fpromise();
            resultFpromise.WithName(Name);

            ActionHandlers(resultFpromise,
                () =>
                {
                },
                ex => Fpromise.PropagateUnhandledException(this, ex)
            );
        }

        /// <summary>
        /// Set the name of the promise, useful for debugging.
        /// </summary>
        public IFpromise WithName(string name)
        {
            this.Name = name;
            return this;
        }

        /// <summary>
        /// Handle errors for the promise. 
        /// </summary>
        public IFpromise Catch(Action<Exception> onRejected)
        {
            //            Argument.NotNull(() => onRejected);

            var resultFpromise = new Fpromise();
            resultFpromise.WithName(Name);

            Action resolveHandler = () =>
            {
                resultFpromise.Resolve();
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
        public IFpromise<ConvertedT> Then<ConvertedT>(Func<IFpromise<ConvertedT>> onResolved)
        {
            return Then(onResolved, null);
        }

        /// <summary>
        /// Add a resolved callback that chains a non-value promise.
        /// </summary>
        public IFpromise Then(Func<IFpromise> onResolved)
        {
            return Then(onResolved, null);
        }

        /// <summary>
        /// Add a resolved callback.
        /// </summary>
        public IFpromise Then(Action onResolved)
        {
            return Then(onResolved, null);
        }

        /// <summary>
        /// Add a resolved callback and a rejected callback.
        /// The resolved callback chains a value promise (optionally converting to a different value type).
        /// </summary>
        public IFpromise<ConvertedT> Then<ConvertedT>(Func<IFpromise<ConvertedT>> onResolved, Action<Exception> onRejected)
        {
            // This version of the function must supply an onResolved.
            // Otherwise there is now way to get the converted value to pass to the resulting promise.
            //            Argument.NotNull(() => onResolved);

            var resultFpromise = new Fpromise<ConvertedT>();
            resultFpromise.WithName(Name);

            Action resolveHandler = () =>
            {
                onResolved()
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
        public IFpromise Then(Func<IFpromise> onResolved, Action<Exception> onRejected)
        {
            var resultFpromise = new Fpromise();
            resultFpromise.WithName(Name);

            Action resolveHandler = () =>
            {
                if (onResolved != null)
                {
                    onResolved()
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
        public IFpromise Then(Action onResolved, Action<Exception> onRejected)
        {
            var resultFpromise = new Fpromise();
            resultFpromise.WithName(Name);

            Action resolveHandler = () =>
            {
                if (onResolved != null)
                {
                    onResolved();
                }

                resultFpromise.Resolve();
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
        /// Helper function to invoke or register resolve/reject handlers.
        /// </summary>
        private void ActionHandlers(IRejectable resultFpromise, Action resolveHandler, Action<Exception> rejectHandler)
        {
            if (CurState == FpromiseState.Resolved)
            {
                InvokeResolveHandler(resolveHandler, resultFpromise);
            }
            else if (CurState == FpromiseState.Rejected)
            {
                InvokeRejectHandler(rejectHandler, resultFpromise, rejectionException);
            }
            else
            {
                AddResolveHandler(resolveHandler, resultFpromise);
                AddRejectHandler(rejectHandler, resultFpromise);
            }
        }

        /// <summary>
        /// Chain an enumerable of promises, all of which must resolve.
        /// The resulting promise is resolved when all of the promises have resolved.
        /// It is rejected as soon as any of the promises have been rejected.
        /// </summary>
        public IFpromise ThenAll(Func<IEnumerable<IFpromise>> chain)
        {
            return Then(() => Fpromise.All(chain()));
        }

        /// <summary>
        /// Chain an enumerable of promises, all of which must resolve.
        /// Converts to a non-value promise.
        /// The resulting promise is resolved when all of the promises have resolved.
        /// It is rejected as soon as any of the promises have been rejected.
        /// </summary>
        public IFpromise<IEnumerable<ConvertedT>> ThenAll<ConvertedT>(Func<IEnumerable<IFpromise<ConvertedT>>> chain)
        {
            return Then(() => Fpromise<ConvertedT>.All(chain()));
        }

        /// <summary>
        /// Returns a promise that resolves when all of the promises in the enumerable argument have resolved.
        /// Returns a promise of a collection of the resolved results.
        /// </summary>
        public static IFpromise All(params IFpromise[] promises)
        {
            return All((IEnumerable<IFpromise>)promises); // Cast is required to force use of the other All function.
        }

        /// <summary>
        /// Returns a promise that resolves when all of the promises in the enumerable argument have resolved.
        /// Returns a promise of a collection of the resolved results.
        /// </summary>
        public static IFpromise All(IEnumerable<IFpromise> promises)
        {
            var promisesArray = promises.ToArray();
            if (promisesArray.Length == 0)
            {
                return Fpromise.Resolved();
            }

            var remainingCount = promisesArray.Length;
            var resultFpromise = new Fpromise();
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
                  .Then(() =>
                  {
                      --remainingCount;
                      if (remainingCount <= 0)
                      {
                              // This will never happen if any of the promises errorred.
                              resultFpromise.Resolve();
                      }
                  })
                  .Done();
            });

            return resultFpromise;
        }

        /// <summary>
        /// Chain a sequence of operations using promises.
        /// Reutrn a collection of functions each of which starts an async operation and yields a promise.
        /// Each function will be called and each promise resolved in turn.
        /// The resulting promise is resolved after each promise is resolved in sequence.
        /// </summary>
        public IFpromise ThenSequence(Func<IEnumerable<Func<IFpromise>>> chain)
        {
            return Then(() => Sequence(chain()));
        }

        /// <summary>
        /// Chain a number of operations using promises.
        /// Takes a number of functions each of which starts an async operation and yields a promise.
        /// </summary>
        public static IFpromise Sequence(params Func<IFpromise>[] fns)
        {
            return Sequence((IEnumerable<Func<IFpromise>>)fns);
        }

        /// <summary>
        /// Chain a sequence of operations using promises.
        /// Takes a collection of functions each of which starts an async operation and yields a promise.
        /// </summary>
        public static IFpromise Sequence(IEnumerable<Func<IFpromise>> fns)
        {
            return fns.Aggregate(
                Fpromise.Resolved(),
                (prevFpromise, fn) =>
                {
                    return prevFpromise.Then(() => fn());
                }
            );
        }

        /// <summary>
        /// Takes a function that yields an enumerable of promises.
        /// Returns a promise that resolves when the first of the promises has resolved.
        /// </summary>
        public IFpromise ThenRace(Func<IEnumerable<IFpromise>> chain)
        {
            return Then(() => Fpromise.Race(chain()));
        }

        /// <summary>
        /// Takes a function that yields an enumerable of promises.
        /// Converts to a value promise.
        /// Returns a promise that resolves when the first of the promises has resolved.
        /// </summary>
        public IFpromise<ConvertedT> ThenRace<ConvertedT>(Func<IEnumerable<IFpromise<ConvertedT>>> chain)
        {
            return Then(() => Fpromise<ConvertedT>.Race(chain()));
        }

        /// <summary>
        /// Returns a promise that resolves when the first of the promises in the enumerable argument have resolved.
        /// Returns the value from the first promise that has resolved.
        /// </summary>
        public static IFpromise Race(params IFpromise[] promises)
        {
            return Race((IEnumerable<IFpromise>)promises); // Cast is required to force use of the other function.
        }

        /// <summary>
        /// Returns a promise that resolves when the first of the promises in the enumerable argument have resolved.
        /// Returns the value from the first promise that has resolved.
        /// </summary>
        public static IFpromise Race(IEnumerable<IFpromise> promises)
        {
            var promisesArray = promises.ToArray();
            if (promisesArray.Length == 0)
            {
                throw new ApplicationException("At least 1 input promise must be provided for Race");
            }

            var resultFpromise = new Fpromise();
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
                  .Then(() =>
                  {
                      if (resultFpromise.CurState == FpromiseState.Pending)
                      {
                          resultFpromise.Resolve();
                      }
                  })
                  .Done();
            });

            return resultFpromise;
        }

        /// <summary>
        /// Convert a simple value directly into a resolved promise.
        /// </summary>
        public static IFpromise Resolved()
        {
            var promise = new Fpromise();
            promise.Resolve();
            return promise;
        }

        /// <summary>
        /// Convert an exception directly into a rejected promise.
        /// </summary>
        public static IFpromise Rejected(Exception ex)
        {
            //            Argument.NotNull(() => ex);

            var promise = new Fpromise();
            promise.Reject(ex);
            return promise;
        }

        /// <summary>
        /// Raises the UnhandledException event.
        /// </summary>
        internal static void PropagateUnhandledException(object sender, Exception ex)
        {
            if (unhandlerException != null)
            {
                unhandlerException(sender, new ExceptionEventArgs(ex));
            }
        }
    }

    /// <summary>
    /// Arguments to the UnhandledError event.
    /// </summary>
    public class ExceptionEventArgs : EventArgs
    {
        internal ExceptionEventArgs(Exception exception)
        {
            //            Argument.NotNull(() => exception);

            this.Exception = exception;
        }

        public Exception Exception
        {
            get;
            private set;
        }
    }

    /// <summary>
    /// Represents a handler invoked when the promise is rejected.
    /// </summary>
    public struct RejectHandler
    {
        /// <summary>
        /// Callback fn.
        /// </summary>
        public Action<Exception> callback;

        /// <summary>
        /// The promise that is rejected when there is an error while invoking the handler.
        /// </summary>
        public IRejectable rejectable;
    }
}

