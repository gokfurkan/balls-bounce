using System;
using UnityEngine;

namespace Game.Dev.Scripts
{
    [Serializable]
    public class SaveData
    {
        //Set
        public int level;
        public int moneys = 10;

        [Space(10)]
        public bool sound;
        public bool haptic;

        //Upgrade
        [Space(10)]
        public int minBallLevel = 0;
        public int havePadAmount = 1;

        [Space(10)]
        public int addPadStartCost = 700;
        public int addBallStartCost = 5;
        public int mergeBallStartCost = 10;

        [Space(5)]
        public int addPadUpgradeLevel;
        public int addBallUpgradeLevel;
        public int mergeBallUpgradeLevel;

        [Space(5)]
        public int addPadCostIncrease = 1400;
        public int addBallCostIncrease = 10;
        public int mergeBallCostIncrease = 20;
    }
}