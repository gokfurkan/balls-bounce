﻿using DG.Tweening;
using Game.Dev.Scripts;
using TMPro;
using UnityEngine;

namespace Template.Scripts
{
    public class EconomyManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private Money moneyPrefab;
        [SerializeField] private RectTransform spawnPos;
        [SerializeField] private RectTransform targetPos;

        private int oldMoneyTarget, newMoneyTarget;
        
        private void OnEnable()
        {
            BusSystem.OnAddMoneys += AddMoneys;
            BusSystem.OnResetMoneys += ResetMoneys;
            BusSystem.OnSetMoneys += SetMoneyText;
            BusSystem.OnSpawnMoneys += SpawnMoneys;
        }

        private void OnDisable()
        {
            BusSystem.OnAddMoneys -= AddMoneys;
            BusSystem.OnResetMoneys -= ResetMoneys;
            BusSystem.OnSetMoneys -= SetMoneyText;
            BusSystem.OnSpawnMoneys -= SpawnMoneys;
        }

        private void Start()
        {
            BusSystem.CallSetMoneys();
        }
        
        private void AddMoneys(int amount)
        {
            var oldAmount =  SaveManager.instance.saveData.moneys;
            var newAmount = oldAmount + amount;

            oldMoneyTarget = oldAmount;
            newMoneyTarget = newAmount;

            SaveManager.instance.saveData.moneys = newAmount;
            
            if (amount > 0)
            {
                SaveManager.instance.saveData.totalEarnedMoneys += amount;
            }
            
            SetMoneyText();
        }

        private void ResetMoneys()
        {
            SaveManager.instance.saveData.moneys = 0;
            SaveManager.instance.Save();

            SetMoneyText();
        }

        private void SetMoneyText()
        {
            if (oldMoneyTarget == 0 || newMoneyTarget == 0)
            {
                oldMoneyTarget = 0;
                newMoneyTarget = SaveManager.instance.saveData.moneys;
            }
            
            if (InitializeManager.instance.gameSettings.economyOptions.useMoneyAnimation)
            {
                AnimateMoneyText(oldMoneyTarget, newMoneyTarget);
            }
            else
            {
                moneyText.text = MoneyCalculator.NumberToStringFormatter(newMoneyTarget);
            }
            
            SaveManager.instance.Save();
            BusSystem.CallRefreshUpgradeValues();
            BusSystem.CallRefreshLevelProgress();
        }
        
        private void SpawnMoneys()
        {
            for (int i = 0; i < InitializeManager.instance.gameSettings.economyOptions.spawnMoneyAmount; i++)
            {
                var money = Instantiate(moneyPrefab, spawnPos);
                money.InitMoney(targetPos);
            }
        }
            
        private void AnimateMoneyText(int startAmount, int targetAmount)
        {
            DOTween.To(() => startAmount, x => startAmount = x, targetAmount, InitializeManager.instance.gameSettings.economyOptions.moneyAnimationDuration)
                .OnUpdate(() => moneyText.text = MoneyCalculator.NumberToStringFormatter(startAmount))
                .SetEase(Ease.Linear)
                .SetUpdate(true)
                .SetSpeedBased(false);
        }
    }
}