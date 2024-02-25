using System.Collections.Generic;
using Sirenix.OdinInspector;
using Template.Scripts;
using UnityEngine;

namespace Game.Dev.Scripts
{
    public class PadManager : Singleton<PadManager>
    {
        [ReadOnly] public int totalPadAmount;
        [ReadOnly] public List<GameObject> pads;

        public void InitPads(Transform padHolder)
        {
            for (int i = 0; i < padHolder.childCount; i++)
            {
                pads.Add(padHolder.GetChild(i).gameObject);
            }

            totalPadAmount = pads.Count;

            ActivatePads();
        }
        
        public void ActivatePads()
        {
            pads.ToggleActivateAll(false);
            for (int i = 0; i < SaveManager.instance.saveData.havePadAmount; i++)
            {
                pads[i].SetActive(true);
            }
        }

        public bool CanUpgrade()
        {
            return !(SaveManager.instance.saveData.havePadAmount >= totalPadAmount);
        }
    }
}