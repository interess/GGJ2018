using FFramework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DZ
{
    public class MobUnit : FUnit
    {
        public string gameSceneName;

        public override void FInit()
        {
            if (!SceneManager.GetSceneByName(gameSceneName).IsValid())
            {
                SceneManager.LoadSceneAsync(gameSceneName, LoadSceneMode.Additive);
            }
        }
    }
}
