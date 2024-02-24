using System;

namespace Game.Dev.Scripts
{
    [Serializable]
    public class SaveData
    {
        //Set
        public int level;
        public int moneys;

        public bool sound;
        public bool haptic;

        //Upgrade
        public int minBallLevel = 0;
        public int havePadAmount = 1;

        public int padUpgradeLevel;
        public int addBallUpgradeLevel;
        public int mergeBallUpgradeBall;
        
    }
}