using UnityEngine;
using UnityEngine.Assertions;

namespace DZ.Game.Scripts
{
    public class StageManagerUnit : MonoBehaviour
    {
        public CameraUnit loadingCameraUnit { get; private set; }
        public CameraUnit gameCameraUnit { get; private set; }

        public void Init()
        {
            var cameraUnits = GameObject.FindObjectsOfType<CameraUnit>();
            if (cameraUnits.Length > 2)
            {
                Debug.Log("There are more than 2 CameraUnits on the scene. It is not expected.");
            }

            foreach (var unit in cameraUnits)
            {
                if (unit.id == "Loading")
                {
                    loadingCameraUnit = unit;
                }
                else
                {
                    gameCameraUnit = unit;
                }
            }

            Assert.IsNotNull(loadingCameraUnit);
            Assert.IsNotNull(gameCameraUnit);
        }

        public void SetLoaded(bool value)
        {
            var loadingAudioListener = loadingCameraUnit.camera.GetComponent<AudioListener>();
            var gameAudioListener = gameCameraUnit.camera.GetComponent<AudioListener>();
            loadingAudioListener.enabled = !value;
            gameAudioListener.enabled = value;
        }
    }
}
