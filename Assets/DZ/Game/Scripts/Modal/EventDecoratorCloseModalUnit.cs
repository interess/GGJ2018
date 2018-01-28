using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DZ.Game.Scripts
{
    public class EventDecoratorCloseModalUnit : MonoBehaviour, IEventDecorator
    {
        private string __modalId;
        private string _modalId
        {
            get
            {
                if (__modalId == null)
                {
                    var parentModalUnit = transform.GetComponentInParent<ModalUnit>();
                    if (parentModalUnit == null)
                    {
                        Debug.LogError("Can't close modal. Parent ModalUnit not found.");
                        __modalId = null;
                    }

                    __modalId = parentModalUnit.id;
                }

                return __modalId;
            }
        }

        public void Decorate(InputEntity entity)
        {
            entity.modalCloseEvent = true;
            if (!string.IsNullOrEmpty(_modalId)) entity.modalId = _modalId;
        }
    }
}
