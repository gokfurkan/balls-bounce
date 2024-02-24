using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Template.Scripts;
using TMPro;
using UnityEngine;

namespace Game.Dev.Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        public LoadOptions loadOptions;
        public GamePlayOptions gamePlayOptions;
        public EconomyOptions economyOptions;
        public UIOptions uiOptions;
        public BallManagerOptions ballManagerOptions;
        public UpgradeOptions upgradeOptions;
    }

    [Serializable]
    public class LoadOptions
    {
        public SceneType nextSceneAfterLoad;
        public List<LoadFillOptions> loadFillOption;
    }

    [Serializable]
    public class GamePlayOptions
    {
        public bool vSyncEnabled;
        public int targetFPS;
    }

    [Serializable]
    public class EconomyOptions
    {
        public bool useMoneyAnimation;
        [ShowIf(nameof(useMoneyAnimation))]
        public float moneyAnimationDuration;

        [Space(10)] 
        public int winIncome;
        public int loseIncome;
        
        [Space(10)] 
        public int spawnMoneyAmount;
    }

    [Serializable]
    public class UIOptions
    {
        public float winPanelDelay;
        public float losePanelDelay;
        public float endContinueDelay;
        
        [Space(10)]
        public string levelText;

        [Space(10)]
        public bool hasEndPanelLevel;
        public string levelCompletedText;
        public string levelFailedText;

        [Space(10)] 
        public TMP_FontAsset  textFont;
    }

    [Serializable]
    public class BallManagerOptions
    {
        [Header("Merge")] 
        public float moveToMergePointDuration;
        public float mergeMoveDuration;
        public float afterMergeMoveUpDuration;
    }

    [Serializable]
    public class UpgradeOptions
    {
        public bool IsActive;
        public bool IsChangeableActive;

        [Header("Upgrade Button Curve Values")] 
        
        [Space(5)]

        [ShowIf("IsChangeableActive")]
        public int BoostCostTime = 3;
        [ShowIf("IsChangeableActive")]
        public float BoostCostValue = 150;
        // [ShowIf("IsChangeableActive")]
        // public int BoostWhatWeGetTime = 3;
        // [ShowIf("IsChangeableActive")]
        // public float BoostWhatWeGetValue = 3;

        
        [Space(5)]

        [ShowIf("IsChangeableActive")]
        public int PowerCostTime = 3;
        [ShowIf("IsChangeableActive")]
        public float PowerCostValue = 150;
        // [ShowIf("IsChangeableActive")]
        // public int PowerWhatWeGetTime = 3;
        // [ShowIf("IsChangeableActive")]
        // public float PowerWhatWeGetValue = 3;
        
        [Space(5)]

        [ShowIf("IsChangeableActive")]
        public int IncomeCostTime = 3;
        [ShowIf("IsChangeableActive")]
        public float IncomeCostValue = 150;
        // [ShowIf("IsChangeableActive")]
        // public int IncomeWhatWeGetTime = 3;
        // [ShowIf("IsChangeableActive")]
        // public float IncomeWhatWeGetValue = 3;
    
        
        [Header("Texts")]
        public string MaxText = "MAX ";
        public string LevelText = "Lvl. ";
        public string CostText = " $";

        [Space(5)]
        public int TutorialGetMoney = 117;
    }
}