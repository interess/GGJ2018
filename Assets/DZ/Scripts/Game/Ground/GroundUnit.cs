using FFramework;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace DZ
{
	public class GroundUnit : FUnit
	{
		public override void FCheck()
		{
			Assert.IsNotNull(_fcontext.stageManagerUnit);
		}

		public override void FInit() { }

		[FreakingEditor.FbuttonEditor]
		[FreakingEditor.FbuttonPlay]
		public void DoSome()
		{
			Debug.Log("FUCKING BUTTON WORKS!");
		}
	}
}
