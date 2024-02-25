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
            BusSystem.OnRefreshUpgradeValues += SetProgress;
        }

        private void OnDisable()
        {
            BusSystem.OnRefreshUpgradeValues -= SetProgress;
        }

        private void Start()
        {
            nextLevelButton.SetActive(false);
        }

        private void SetProgress()
        {
            var progress = SaveManager.instance.saveData.totalEarnedMoneys;
            targetText.text = MoneyCalculator.NumberToStringFormatter(progress) + " / " + MoneyCalculator.NumberToStringFormatter(targetProgress);
            fill.fillAmount = (float)progress / targetProgress;
            if (fill.fillAmount >= 1)
            {
                nextLevelButton.SetActive(true);
            }
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