using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DZ.Game.Scripts
{
    public class StickUnit : MonoBehaviour
    {
        private Animator __animator;
        protected Animator _animator { get { if (__animator == null) { __animator = GetComponent<Animator>(); } return __animator; } }

        public void Initialize()
        {

        }

        public void Move(int direction)
        {
            _animator.SetTrigger("Move_" + direction);
        }

        public void SetSelected(bool value)
        {
            _animator.SetBool("Select", value);
        }
    }
}
