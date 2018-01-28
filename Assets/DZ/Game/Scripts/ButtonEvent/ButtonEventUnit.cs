using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DZ.Game.Scripts
{
    public class ButtonEventUnit : MonoBehaviour
    {
        Button _button;
        protected Button button { get { if (_button == null) _button = GetComponent<Button>(); return _button; } }

        void Start()
        {
            button.onClick.AddListener(HandleClickedButton);
        }

        protected virtual void HandleClickedButton()
        {
            var entity = Contexts.input.CreateEventEntity();
            var decorators = GetComponents<IEventDecorator>();

            foreach (var decorator in decorators)
            {
                decorator.Decorate(entity);
            }
        }
    }
}
