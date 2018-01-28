using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DZ.Game.Scripts
{
	public class EffectGroupUnit : EffectUnit
	{
		private EffectUnit[] _effectUnits;

		public override void Initialize()
		{
			if (_isInitialized) { return; }
			base.Initialize();

			_effectUnits = GetComponentsInChildren<EffectUnit>();

			foreach (var unit in _effectUnits)
			{
				if (unit != this)
				{
					unit.isGroupPart = true;
					unit.Initialize();
				}
			}
		}

		public override EffectUnit Play()
		{
			base.Play();
			if (_effectUnits == null) { return this; }
			foreach (var unit in _effectUnits)
			{
				if (unit != this)
				{
					unit.Play();
				}
			}
			return this;
		}

		public override EffectUnit Stop()
		{
			base.Stop();
			if (_effectUnits == null) { return this; }
			foreach (var unit in _effectUnits)
			{
				if (unit != this)
				{
					unit.Stop();
				}
			}
			return this;
		}
	}
}
