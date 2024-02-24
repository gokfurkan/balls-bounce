using System;
using Game.Dev.Scripts;
using UnityEngine;

namespace Game.Dev
{
    public class LevelController : MonoBehaviour
    {
        public Transform padHolder;

        private void Start()
        {
            PadManager.instance.InitPads(padHolder);
        }
    }
}