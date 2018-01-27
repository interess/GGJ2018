using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DZ.Game.Scripts
{
    public class EventDecoratorEventIdUnit : MonoBehaviour, IEventDecorator
    {
        public string eventId;

        public void Decorate(InputEntity entity)
        {
            entity.eventId = eventId;
        }
    }
}
