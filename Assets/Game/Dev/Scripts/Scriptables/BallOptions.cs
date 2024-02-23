using UnityEngine;

namespace Game.Dev.Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "BallOptions", menuName = "ScriptableObjects/BallOptions")]
    public class BallOptions : ScriptableObject
    {
        public int incomeAmount;
    }
}