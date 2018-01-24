using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FFramework
{
	public interface IFTickable
	{
		void FTick();
	}

	public interface IFTickableLate
	{
		void FTickLate();
	}

	public interface IFTickableFixed
	{
		void FTickFixed();
	}
}
