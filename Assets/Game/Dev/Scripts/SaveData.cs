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
        

        //Get
        public int GetLevel()
        {
            return level;
        }

        public int GetMoneys()
        {
            return moneys;
        }

        public bool GetSound()
        {
            return sound;
        }

        public bool GetHaptic()
        {
            return haptic;
        }
    }
}