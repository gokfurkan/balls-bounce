using DG.Tweening;
using Game.Dev.Scripts.Ball;
using UnityEngine;

namespace Game.Dev.Scripts
{
    public class PadController : MonoBehaviour , Interacteable
    {
        public float bendAmount = 0.085f;
        public float bendDuration = 0.5f;
        
        [Space(10)]
        public MeshRenderer meshRenderer;

        private Sequence sequence;
        private static readonly int Amplitude = Shader.PropertyToID("_Amplitude");

        public void Interact(BallController ball)
        {
            var incomeAmount = ball.ballOptions.incomeAmount;
            BusSystem.CallAddMoneys(incomeAmount);
            BusSystem.CallSpawnIncomeVisual(ball.transform , incomeAmount);
            
            sequence = DOTween.Sequence();
            sequence.Append(meshRenderer.material.DOFloat(bendAmount, Amplitude, bendDuration));
            sequence.Append(meshRenderer.material.DOFloat(0f, Amplitude, bendDuration));
            sequence.SetAutoKill(true);
        }
    }
}