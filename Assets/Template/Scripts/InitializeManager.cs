using System;
using Game.Dev.Scripts;
using Game.Dev.Scripts.Scriptables;
using UnityEngine;

namespace Template.Scripts
{
    public class InitializeManager : PersistentSingleton<InitializeManager>
    {
        public GameSettings gameSettings;

        protected override void Initialize()
        {
            base.Initialize();

            SetFrameRateSettings();

            AddOperation(typeof(SaveManager));
            AddOperation(typeof(Development));
            AddOperation(typeof(HapticManager));
        }
        
        private void AddOperation(Type type)
        {
            gameObject.AddComponent(type);
        }
        
        private void SetFrameRateSettings()
        {
            QualitySettings.vSyncCount = gameSettings.gamePlayOptions.vSyncEnabled ? 1 : 0;
            Application.targetFrameRate = gameSettings.gamePlayOptions.targetFPS;
        }
    }
}
