using Template.Scripts;
using UnityEngine;

namespace Game.Dev.Scripts
{
    public class LevelController : MonoBehaviour
    {
        public Transform padHolder;
        public int targetProgress;
        public int startPadAmount;

        private void Start()
        {
            if (SaveManager.instance.saveData.havePadAmount == 0)
            {
                SaveManager.instance.saveData.havePadAmount = startPadAmount;
            }
            
            PadManager.instance.InitPads(padHolder);
            LevelProgressManager.instance.InitLevelProgress(targetProgress);
        }
    }
}