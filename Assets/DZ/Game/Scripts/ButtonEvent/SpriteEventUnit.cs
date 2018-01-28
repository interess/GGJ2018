using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DZ.Game.Scripts
{
    public class SpriteEventUnit : MonoBehaviour
    {
        void OnMouseUpAsButton()
        {
            if (Contexts.state.HasModalActive()) { return; }

            var entity = Contexts.input.CreateEventEntity();
            var decorators = GetComponents<IEventDecorator>();

            foreach (var decorator in decorators)
            {
                decorator.Decorate(entity);
            }
        }
    }
}
