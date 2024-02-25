using Game.Dev.Scripts.Interfaces;
using UnityEngine;

namespace Game.Dev.Scripts
{
    public class ReSpawnerTrigger : MonoBehaviour , Interacteable
    {
        public void Interact(BallController ballController)
        {
            BusSystem.CallReSpawnBall(ballController);
        }
    }
}