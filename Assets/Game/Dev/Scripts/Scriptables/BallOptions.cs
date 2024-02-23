using UnityEngine;

namespace Game.Dev.Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "BallOptions", menuName = "ScriptableObjects/BallOptions")]
    public class BallOptions : ScriptableObject
    {
        public int incomeAmount;
        
        [Space(10)]
        public LayerMask interactLayers;
        public Vector2Int startForceXRandom;
        public float firstInteractionForceY;
    }
}