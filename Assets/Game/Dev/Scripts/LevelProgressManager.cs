using System;
using Sirenix.OdinInspector;
using Template.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Dev.Scripts
{
    public class LevelProgressManager : Singleton<LevelProgressManager>
    {
        public Image fill;
        public TextMeshProUGUI targetText;
        public GameObject nextLevelButton;
        
        [Space(10)]
        [ReadOnly] public int targetProgress;

        private void OnEnable()
        {
            BusSystem.OnRefreshLevelProgress += SetProgress;
        }

        private void OnDisable()
        {
            BusSystem.OnRefreshLevelProgress -= SetProgress;
        }

        protected override void Initialize()
        {
            base.Initialize();
            
            nextLevelButton.SetActive(false);
        }

        private void SetProgress()
        {
            var progress = SaveManager.instance.saveData.totalEarnedMoneys;
            targetText.text = MoneyCalculator.NumberToStringFormatter(progress) + " / " + MoneyCalculator.NumberToStringFormatter(targetProgress);
            
            fill.fillAmount = (float)progress / targetProgress;
            nextLevelButton.SetActive(fill.fillAmount >= 1);
        }

        public void InitLevelProgress(int target)
        {
            targetProgress = target;
            SetProgress();
        }

        public void LevelWin()
        {
            BusSystem.CallLevelEnd(true);
        }
    }
}