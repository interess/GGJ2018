using FFramework;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace FFramework
{
	public partial class FContext
	{
		public DZ.StageManagerUnit stageManagerUnit;
	}
}

namespace DZ
{
	/// Manager that controls cameras, shows or hides UI.
	public class StageManagerUnit : FUnit
	{
		public override void FInit()
		{
			_fcontext.stageManagerUnit = this;
		}
	}
}
