using System;
using System.Collections.Generic;
using Template.Scripts;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Dev.Scripts.Ball
{
    public class BallManager : MonoBehaviour
    {
        public List<BallController> balls;

        [Space(10)] 
        public Transform spawnPos;
        public Transform ballHolder;

        private void OnEnable()
        {
            BusSystem.OnAddBall += AddBall;
            BusSystem.OnMergeBall += MergeBall;
            BusSystem.OnReSpawnBall += ReSpawnBall;
        }

        private void OnDisable()
        {
            BusSystem.OnAddBall -= AddBall;
            BusSystem.OnMergeBall -= MergeBall;
            BusSystem.OnReSpawnBall -= ReSpawnBall;
        }

        private void AddBall()
        {
            var createdBall = Pooling.instance.poolObjects[(int)PoolType.Ball].GetItem();
            createdBall.transform.SetParent(ballHolder , true);

            var createdBallController = createdBall.GetComponent<BallController>();
            balls.Add(createdBallController);
            
            SpawnBall(createdBallController);
        }

        private void MergeBall()
        {
            
        }

        private void SpawnBall(BallController ball)
        {
            ball.transform.position = spawnPos.position;
            ball.gameObject.SetActive(true);
            ball.InitBall();
        }

        private void ReSpawnBall(BallController ball)
        {
            ball.gameObject.SetActive(false);
            ball.ResetMovement();
            ball.transform.position = spawnPos.position;
            ball.gameObject.SetActive(true);
            ball.InitBall();
        }
    }
}