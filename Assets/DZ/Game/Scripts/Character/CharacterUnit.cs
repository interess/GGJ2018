using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DZ.Game.Scripts
{
    public class CharacterUnit : MonoBehaviour
    {
        private Animator __animator;
        protected Animator _animator { get { if (__animator == null) { __animator = GetComponent<Animator>(); } return __animator; } }

        private float __timeSinceLastExchale;

        public void Initialize()
        {

        }

        public void MoveStick()
        {
            _animator.SetTrigger("Switch");
        }

        private void Update()
        {
            __timeSinceLastExchale = Random.Range(3f, 12f);

            __timeSinceLastExchale -= Time.deltaTime;

            if (__timeSinceLastExchale <= 0)
            {
                _animator.SetTrigger("Exhale");
            }
        }
    }
}
