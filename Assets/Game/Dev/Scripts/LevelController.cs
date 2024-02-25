using UnityEngine;

namespace Game.Dev.Scripts
{
    public class LevelController : MonoBehaviour
    {
        public Transform padHolder;
        public int targetProgress;

        private void Start()
        {
            PadManager.instance.InitPads(padHolder);
            LevelProgressManager.instance.InitLevelProgress(targetProgress);
        }
    }
}