using System.Collections.Generic;
using Sirenix.OdinInspector;
using Template.Scripts;
using UnityEngine;

namespace Game.Dev.Scripts
{
    public class UpgradeManager : Singleton<UpgradeManager>
    {
        #region Pad
        
        public void AddPad()
        {
            if (SaveManager.instance.saveData.havePadAmount + 1 > PadManager.instance.pads.Count)
            {
                Debug.Log("Max pad upgrade");
                return;
            }
            
            SaveManager.instance.saveData.havePadAmount++;
            SaveManager.instance.Save();
            
            PadManager.instance.ActivatePads();
        }
        
        #endregion

        #region Ball

        public void AddBall()
        {
            BusSystem.CallAddNewBall(SaveManager.instance.saveData.minBallLevel);
        }

        public void MergeBall()
        {
            BusSystem.CallMergeBall();
        }

        #endregion
    }
}