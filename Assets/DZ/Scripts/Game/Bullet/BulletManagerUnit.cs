using System.Collections;
using System.Collections.Generic;
using FFramework;
using UnityEngine;
using UnityEngine.Assertions;

namespace DZ
{
	public class BulletManagerUnit : FUnit
	{
		public GameObject bulletPrefab;
		private FactoryUnit __bulletFactoryUnit;

		public override void FCheck()
		{
			Assert.IsNotNull(bulletPrefab);
		}

		public override void FInit()
		{
			__bulletFactoryUnit = FactoryUnit.Create(gameObject, bulletPrefab, 2);
		}

		[FreakingEditor.FbuttonPlay]
		public void SpawBullet()
		{

		}
	}
}
