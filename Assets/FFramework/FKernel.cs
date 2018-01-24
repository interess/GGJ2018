using System.Collections;
using System.Collections.Generic;
using Freaking.Extensions.GameObject;
using UnityEngine;

namespace FFramework
{
	public class FKernel : MonoBehaviour
	{
		private static FKernel __instance;

		private FContext __fcontext;

		private HashSet<FUnit> __lookupInitableUnit;
		private HashSet<FUnit> __lookupInitableUnitSafe;

		private HashSet<IFTickable> __lookupTickable;
		private HashSet<IFTickableLate> __lookupTickableLate;
		private HashSet<IFTickableFixed> __lookupTickableFixed;

		private void Awake()
		{
			if (__instance != null)
			{
				Debug.LogErrorFormat("Kernel already exists. Two Kernel scripts can't exists at the same time | {0}", gameObject.GetPath());
				gameObject.SetActive(false);
				return;
			}

			__instance = this;

			__lookupInitableUnit = new HashSet<FUnit>();
			__lookupInitableUnitSafe = new HashSet<FUnit>();
			__lookupTickable = new HashSet<IFTickable>();
			__lookupTickableLate = new HashSet<IFTickableLate>();
			__lookupTickableFixed = new HashSet<IFTickableFixed>();

			__fcontext = new FContext();

			UnityEngine.Assertions.Assert.raiseExceptions = true;
		}

		private void Update()
		{
			if (__lookupInitableUnit.Count > 0)
			{
				__lookupInitableUnitSafe = new HashSet<FUnit>(__lookupInitableUnit);
				__lookupInitableUnit.Clear();

				Debug.Log("Update Initables");

				foreach (var unit in __lookupInitableUnitSafe)
				{
					var passed = true;

					try
					{
						unit.FCheck();
					}
					catch
					{
						passed = false;
					}

					if (passed)
					{
						Debug.Log("Init funit " + unit.gameObject.name + " | " + unit.GetType().Name);
						unit.___FInternalInit(__fcontext);

						var tickable = unit as IFTickable;
						var tickableLate = unit as IFTickableLate;
						var tickableFixed = unit as IFTickableFixed;

						if (tickable != null) { __lookupTickable.Add(tickable); }
						if (tickableLate != null) { __lookupTickableLate.Add(tickableLate); }
						if (tickableFixed != null) { __lookupTickableFixed.Add(tickableFixed); }
					}
					else
					{
						__lookupInitableUnit.Add(unit);

					}
				}
			}

			if (__lookupTickable.Count > 0)
			{
				foreach (var tickable in __lookupTickable)
				{
					tickable.FTick();
				}
			}
		}

		private void LateUpdate()
		{
			if (__lookupTickableLate.Count > 0)
			{
				foreach (var tickableLate in __lookupTickableLate)
				{
					tickableLate.FTickLate();
				}
			}
		}

		private void FixedUpdate()
		{
			if (__lookupTickableFixed.Count > 0)
			{
				foreach (var tickableFixed in __lookupTickableFixed)
				{
					tickableFixed.FTickFixed();
				}
			}
		}

		public static void Register(FUnit value)
		{
			__instance.__lookupInitableUnit.Add(value);
		}

		public static void Deregister(FUnit value)
		{
			var tickable = value as IFTickable;
			var tickableLate = value as IFTickableLate;
			var tickableFixed = value as IFTickableFixed;

			if (__instance.__lookupInitableUnit.Contains(value))
			{
				__instance.__lookupInitableUnit.Remove(value);
			}

			if (tickable != null)
			{
				if (__instance.__lookupTickable.Contains(tickable))
				{
					__instance.__lookupTickable.Remove(tickable);
				}
			}

			if (tickableLate != null)
			{
				if (__instance.__lookupTickableLate.Contains(tickableLate))
				{
					__instance.__lookupTickableLate.Remove(tickableLate);
				}
			}

			if (tickableFixed != null)
			{
				if (__instance.__lookupTickableFixed.Contains(tickableFixed))
				{
					__instance.__lookupTickableFixed.Remove(tickableFixed);
				}
			}

			value.___FInternalDeinit();
		}
	}
}
