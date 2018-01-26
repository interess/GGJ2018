﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DZ.Game
{
	public class Kernel : Entitas.Gentitas.Kernel
	{
		protected override void Setup()
		{
			Add(new Systems.Common.Before());

			Add(new Systems.Stage.Chain());

			Add(new Systems.Common.After());
			Add(new Systems.Common.Trash());
		}
	}
}