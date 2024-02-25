using Game.Dev.Scripts.Scriptables;
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
            if (!HaveMoney())
            {
                return;
            }

            if (IsMaxLevel())
            {
                return;
            }
            
            OnUpgrade();
            BusSystem.CallAddMoneys(-GetCostAmount());
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
        }
        
        private void Refresh()
        {
            SetUpgradeTexts();
            ControlButtonEnabled();
            IncreaseUpgradeLevel();
        }

        private void ControlButtonEnabled()
        {
            if (!IsMaxLevel() && HaveMoney())
            {
                activeButton.SetActive(true);
                deActiveButton.SetActive(false);
            }
            else
            {
                activeButton.SetActive(false);
                deActiveButton.SetActive(true);
            }
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
                    text.text = MoneyCalculator.NumberToStringFormatter(GetCostAmount()) + upgradeSettings.costText;
                }
            }
        }
        
        private int GetUpgradeLevel()
        {
            var saveData = SaveManager.instance.saveData;
            var upgradeLevel = upgradeType switch
            {
                UpgradeType.AddPad => saveData.padUpgradeLevel,
                UpgradeType.AddBall => saveData.addBallUpgradeLevel,
                UpgradeType.MergeBall => saveData.mergeBallUpgradeBall,
                _ => 0
            };
        
            return upgradeLevel;
        }

        private void IncreaseUpgradeLevel()
        {
            var saveData = SaveManager.instance.saveData;
            switch (upgradeType)
            {
                case UpgradeType.AddPad:
                    saveData.padUpgradeLevel++;
                    break;
                case UpgradeType.AddBall:
                    saveData.addBallUpgradeLevel++;
                    break;
                case UpgradeType.MergeBall:
                    saveData.mergeBallUpgradeBall++;
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
            return 100;
        }
        
        private bool HaveMoney()
        {
            return SaveManager.instance.saveData.moneys >= GetCostAmount();
        }
    }
}