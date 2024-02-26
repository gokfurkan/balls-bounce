using System;
using System.Collections;
using Game.Dev.Scripts.Scriptables;
using MoreMountains.NiceVibrations;
using Template.Scripts;
using TMPro;
using UnityEngine;

namespace Game.Dev.Scripts
{
    public class UpgradeButton : MonoBehaviour
    {
        [SerializeField] private UpgradeType upgradeType;

        [Space(10)]
        [SerializeField] private GameObject activeButton;
        [SerializeField] private GameObject deActiveButton;
        
        [Space(10)]
        [SerializeField] private TextMeshProUGUI[] costTexts;

        private bool canUpgrade = true;
        private UpgradeOptions upgradeSettings;
        
        private void OnEnable()
        {
            BusSystem.OnRefreshUpgradeValues += Refresh;
        }

        private void OnDisable()
        {
            BusSystem.OnRefreshUpgradeValues -= Refresh;
        }

        private void Awake()
        {
            upgradeSettings = InitializeManager.instance.gameSettings.upgradeOptions;
        }

        public void Upgrade()
        {
            if (!canUpgrade)
            {
                return;
            }
            
            if (!HaveMoney())
            {
                return;
            }

            if (IsMaxLevel())
            {
                return;
            }
            
            BusSystem.CallAddMoneys(-GetCostAmount());
            
            OnUpgrade();
        }

        private void OnUpgrade()
        {
            switch (upgradeType)
            {
                case UpgradeType.AddPad:
                    UpgradeManager.instance.AddPad();
                    break;
                case UpgradeType.AddBall:
                    UpgradeManager.instance.AddBall();
                    break;
                case UpgradeType.MergeBall:
                    UpgradeManager.instance.MergeBall();
                    break;
            }
            
            StartCoroutine(ReEnableButtonDelay());
            IncreaseUpgradeLevel();
            Refresh();
            
            AudioManager.instance.Play(AudioType.Pop);
            HapticManager.instance.PlayHaptic(HapticTypes.MediumImpact);
            
            SaveManager.instance.Save();
        }
        
        private void Refresh()
        {
            SetUpgradeTexts();
            ControlButtonEnabled();
        }

        private void ControlButtonEnabled()
        {
            bool canInteract = !IsMaxLevel() && HaveMoney();
            bool canActivate = upgradeType != UpgradeType.MergeBall || BallManager.instance.CanMerge();

            activeButton.SetActive(canInteract && canActivate);
            deActiveButton.SetActive(!canInteract || !canActivate);
        }

        private void SetUpgradeTexts()
        {
            if (IsMaxLevel())
            {
                foreach (var text in costTexts)
                {
                    text.text = upgradeSettings.maxText;
                }
            }
            else
            {
                foreach (var text in costTexts)
                {
                    text.text = upgradeSettings.costText + MoneyCalculator.NumberToStringFormatter(GetCostAmount());
                }
            }
        }

        private void IncreaseUpgradeLevel()
        {
            var saveData = SaveManager.instance.saveData;
            switch (upgradeType)
            {
                case UpgradeType.AddPad:
                    saveData.addPadUpgradeLevel++;
                    if (saveData.addPadUpgradeLevel % upgradeSettings.costValueMod == 0)
                    {
                        saveData.addPadCostIncrease *= upgradeSettings.costMultiplier;
                    }
                    break;
                case UpgradeType.AddBall:
                    saveData.addBallUpgradeLevel++;
                    if (saveData.addBallUpgradeLevel % upgradeSettings.costValueMod == 0)
                    {
                        saveData.addBallCostIncrease *= upgradeSettings.costMultiplier;
                    }
                    break;
                case UpgradeType.MergeBall:
                    saveData.mergeBallUpgradeLevel++;
                    if (saveData.mergeBallUpgradeLevel % upgradeSettings.costValueMod == 0)
                    {
                        saveData.mergeBallCostIncrease *= upgradeSettings.costMultiplier;
                    }
                    break;
            }
        }
        
        private bool IsMaxLevel()
        {
            bool isMaxLevel = false;

            switch (upgradeType)
            {
                case UpgradeType.AddPad:
                    isMaxLevel = !PadManager.instance.CanUpgrade();
                    break;
                case UpgradeType.AddBall:
                    break;
                case UpgradeType.MergeBall:
                    break;
            }

            return isMaxLevel;
        }

        private int GetCostAmount()
        {
            int costAmount = 0;
            SaveData saveData = SaveManager.instance.saveData;
            
            switch (upgradeType)
            {
                case UpgradeType.AddPad:
                    costAmount = saveData.addPadStartCost + (saveData.addPadUpgradeLevel * saveData.addPadCostIncrease);
                    break;
                case UpgradeType.AddBall:
                    costAmount = saveData.addBallStartCost + (saveData.addBallUpgradeLevel * saveData.addBallCostIncrease);
                    break;
                case UpgradeType.MergeBall:
                    costAmount = saveData.mergeBallStartCost + (saveData.mergeBallUpgradeLevel * saveData.mergeBallCostIncrease);
                    break;
            }

            return costAmount;
        }
        
        private bool HaveMoney()
        {
            return SaveManager.instance.saveData.moneys >= GetCostAmount();
        }

        private IEnumerator ReEnableButtonDelay()
        {
            canUpgrade = false;
            yield return new WaitForSeconds(0.5f);
            canUpgrade = true;
        }
    }
}