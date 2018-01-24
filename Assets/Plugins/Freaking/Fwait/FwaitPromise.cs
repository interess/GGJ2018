using System.Collections;
using UnityEngine;

namespace Freaking
{
    public class FwaitPromise
    {
        private System.Action doneAction;

        public void Done(System.Action action)
        {
            doneAction = action;
        }

        public void Resolve()
        {
            doneAction();
        }
    }
}
