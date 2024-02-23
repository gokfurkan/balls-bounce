using System;
using Template.Scripts;
using UnityEngine;

namespace Game.Dev.Scripts
{
    public class UIManager : Singleton<UIManager>
    {
        private void OnEnable()
        {
            BusSystem.OnSpawnIncomeVisual += SpawnIncomeVisual;
        }

        private void OnDisable()
        {
            BusSystem.OnSpawnIncomeVisual -= SpawnIncomeVisual;
        }

        private void SpawnIncomeVisual(Transform pos)
        {
            var createdIncomeVisual = Pooling.instance.poolObjects[(int)PoolType.IncomeVisual].GetItem();
            createdIncomeVisual.transform.position = pos.position;
            createdIncomeVisual.SetActive(true);
            createdIncomeVisual.GetComponent<IncomeVisual>().InitIncome();
        }
    }
}