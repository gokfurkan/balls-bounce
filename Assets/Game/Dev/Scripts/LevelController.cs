using UnityEngine;

namespace Game.Dev.Scripts
{
    public class LevelController : MonoBehaviour
    {
        public Transform padHolder;

        private void Awake()
        {
            PadManager.instance.InitPads(padHolder);
        }
    }
}