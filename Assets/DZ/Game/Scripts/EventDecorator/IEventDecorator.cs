using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DZ.Game.Scripts
{
    public interface IEventDecorator
    {
        void Decorate(InputEntity entity);
    }
}
