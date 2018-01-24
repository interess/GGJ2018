using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FFramework
{
    public abstract class FUnit : MonoBehaviour
    {
        private bool __inited;

        private FContext __fcontext;
        protected FContext _fcontext { get { return __fcontext; } }
        private int __fuid = -1;
        public int fuid
        {
            get
            {
                if (__fuid == -1) { __fuid = FUidGenerator.nextFuid; }
                return __fuid;
            }
        }

        public void ___FInternalInit(FContext context)
        {
            // Protection from repeated init
            if (__inited) { return; }

            __inited = true;
            __fcontext = context;

            FInit();
        }

        public void ___FInternalDeinit()
        {
            // Protection from repeated deinit
            if (!__inited) { return; }

            __inited = false;
            __fcontext = null;

            FDeinit();
        }

        public virtual void FCheck() { }
        public virtual void FInit() { }
        public virtual void FDeinit() { }
        public virtual void FTick() { }
        public virtual void FTickLate() { }
        public virtual void FTickFixed() { }

        private void Awake()
        {
            FKernel.Register(this);
        }

        private void OnDestroy()
        {
            FKernel.Deregister(this);
        }

        public override int GetHashCode()
        {
            return fuid;
        }
    }
}
