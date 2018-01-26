using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DZ.Core
{
	public class Kernel : Entitas.Gentitas.Kernel
	{
		protected override void Setup()
		{
			Add(new Systems.Common.Chain());
		}
	}
}
 