using UnityEngine;
using System;
using System.Collections;

namespace Freaking
{
    public partial class Fpromise
    {
        public static IFpromise WaitUntil(Func<bool> condition)
        {
            var promise = new Fpromise();

            var routine = _ResolveAfterCondition(promise, condition);
            Fcoroutine.Start(routine);

            return promise;
        }

        public static IFpromise WaitForSeconds(float seconds)
        {
            var promise = new Fpromise();

            var routine = _ResolveAfterSeconds(promise, seconds);
            Fcoroutine.Start(routine);

            return promise;
        }

        public static IFpromise WaitUntilOrForSeconds(Func<bool> condition, float secondsToReject)
        {
            var promise = new Fpromise();

            var routine = _ResolveAfterCondition(promise, condition);
            var coroutine = Fcoroutine.Start(routine);

            WaitForSeconds(secondsToReject)
                .Then(() =>
                {
                    if (coroutine != null) Fcoroutine.Stop(coroutine);
                    promise.SoftReject(new Exception("FpromiseWaitUntilOrForSecondsTimeoutException"));
                });


            return promise;
        }

        public static IFpromise WaitForEndOfFrame()
        {
            var promise = new Fpromise();

            var routine = _ResolverAfterEndOfFrame(promise);
            Fcoroutine.Start(routine);

            return promise;
        }


        static IEnumerator _ResolveAfterCondition(Fpromise promise, Func<bool> condition)
        {
            yield return new WaitUntil(condition);

            promise.SoftResolve();
        }

        static IEnumerator _ResolveAfterSeconds(Fpromise promise, float seconds)
        {
            yield return new WaitForSeconds(seconds);

            promise.SoftResolve();
        }

        static IEnumerator _ResolverAfterEndOfFrame(Fpromise promise)
        {
            yield return new WaitForEndOfFrame();

            promise.SoftResolve();
        }

    }
}
