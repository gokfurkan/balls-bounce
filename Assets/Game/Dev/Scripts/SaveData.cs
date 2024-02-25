using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Dev.Scripts
{
    [Serializable]
    public class SaveData
    {
        //Set
        public int level;
        public int moneys = 10;
        public int totalEarnedMoneys;

        [Space(10)]
        public bool sound;
        public bool haptic;

        //Upgrade
        [Space(10)]
        public int minBallLevel = 0;
        public int havePadAmount = 1;

        [Space(10)]
        public int addPadStartCost = 700;
        public int addBallStartCost = 10;
        public int mergeBallStartCost = 10;

        [Space(5)]
        public int addPadUpgradeLevel;
        public int addBallUpgradeLevel;
        public int mergeBallUpgradeLevel;

        [Space(5)]
        public int addPadCostIncrease = 1400;
        public int addBallCostIncrease = 10;
        public int mergeBallCostIncrease = 20;

        [Space(5)]
        public List<int> haveBallLevels = new List<int>();

        public void OnNewLevel()
        {
            moneys = 10;
            totalEarnedMoneys = 0;
            havePadAmount = 1;
            addPadStartCost = 700;
            addBallStartCost = 10;
            mergeBallStartCost = 10;
            addPadUpgradeLevel = 0;
            addBallUpgradeLevel = 0;
            mergeBallUpgradeLevel = 0;
            addPadCostIncrease = 1400;
            addBallCostIncrease = 10;
            mergeBallCostIncrease = 20;
            haveBallLevels = new List<int>();
        }
    }
}