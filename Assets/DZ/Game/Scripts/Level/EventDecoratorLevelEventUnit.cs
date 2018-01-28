using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DZ.Game.Scripts
{
    public class EventDecoratorLevelEventUnit : MonoBehaviour, IEventDecorator
    {
        public void Decorate(InputEntity entity)
        {
            entity.levelEvent = true;
        }
    }
}
