using System;
using System.Collections.Generic;

namespace Freaking
{
    public static class FpromiseExtensions
    {
        public static Fpromise SoftResolve(this Fpromise promise)
        {
            if (promise.IsNotNull() && promise.IsPending()) promise.Resolve();
            return promise;
        }

        public static Fpromise<T> SoftResolve<T>(this Fpromise<T> promise, T data)
        {
            if (promise.IsNotNull() && promise.IsPending()) promise.Resolve(data);
            return promise;
        }


        public static Fpromise SoftReject(this Fpromise promise, Exception exception)
        {
            if (promise.IsNotNull() && promise.IsPending()) promise.Reject(exception);
            return promise;
        }

        public static Fpromise<T> SoftReject<T>(this Fpromise<T> promise, Exception exception)
        {
            if (promise.IsNotNull() && promise.IsPending()) promise.Reject(exception);
            return promise;
        }


        public static bool IsNotNull(this Fpromise promise)
        {
            if (promise != null) return true;
            return false;
        }

        public static bool IsNotNull<T>(this Fpromise<T> promise)
        {
            if (promise != null) return true;
            return false;
        }

        public static bool IsPending(this Fpromise promise)
        {
            if (promise.CurState == FpromiseState.Pending) return true;
            return false;
        }

        public static bool IsPending<T>(this Fpromise<T> promise)
        {
            if (promise.CurState == FpromiseState.Pending) return true;
            return false;
        }


        /// <summary>
        /// Returns true if promise is not null and pending
        /// </summary>
        public static bool Exists(this Fpromise promise)
        {
            if (promise.IsNotNull() && promise.IsPending()) return true;
            return false;
        }

        /// <summary>
        /// Returns true if promise is not null and pending
        /// </summary>
        public static bool Exists<T>(this Fpromise<T> promise)
        {
            if (promise.IsNotNull() && promise.IsPending()) return true;
            return false;
        }

        /// <summary>
        /// Returns true if promise is not null and pending
        /// </summary>
        public static bool Exists(this IFpromise iFpromise)
        {
            var promise = (Fpromise)iFpromise;
            if (promise.IsNotNull() && promise.IsPending()) return true;
            return false;
        }

        /// <summary>
        /// Returns true if promise is not null and pending
        /// </summary>
        public static bool Exists<T>(this IFpromise<T> iFpromise)
        {
            var promise = (Fpromise<T>)iFpromise;
            if (promise.IsNotNull() && promise.IsPending()) return true;
            return false;
        }
    }
}
