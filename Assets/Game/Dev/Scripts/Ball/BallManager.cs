using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Template.Scripts;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Dev.Scripts.Ball
{
    public class BallManager : Singleton<BallManager>
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

        [Button]
        private void MergeBall()
        {
            List<BallController> highestBalls = FindMergeBalls();
            if (highestBalls.Count < 2) return;
           
            for (int i = 0; i < 2; i++)
            {
                balls.Remove(highestBalls[i]);
                Pooling.instance.poolObjects[(int)PoolType.Ball].PutItem(highestBalls[i].gameObject);
            }
        }

        private void SpawnBall(BallController ball)
        {
            ball.transform.position = spawnPos.position;
            ball.gameObject.SetActive(true);
            ball.InitBall();
        }

        private void ReSpawnBall(BallController ball)
        {
            ball.OnReSpawnBall();
            ball.gameObject.SetActive(false);
            ball.transform.position = spawnPos.position;
            ball.gameObject.SetActive(true);
            ball.InitBall();
        }
        
        private List<BallController> FindMergeBalls()
        {
            List<BallController> mergeBalls = new List<BallController>();
            
            Dictionary<int, int> levelCounts = new Dictionary<int, int>();
            
            foreach (BallController ball in balls)
            {
                int ballLevel = ball.ballOptions.level;
        
                if (!levelCounts.TryAdd(ballLevel, 1))
                {
                    levelCounts[ballLevel]++;
                }
            }
            
            var sortedLevels = levelCounts.Keys.OrderByDescending(x => x).ToList();
            
            foreach (int level in sortedLevels)
            {
                if (levelCounts[level] >= 2)
                {
                    foreach (BallController ball in balls)
                    {
                        if (ball.ballOptions.level == level)
                        {
                            mergeBalls.Add(ball);
                            if (mergeBalls.Count >= 2)
                            {
                                break;
                            }
                        }
                    }
                    if (mergeBalls.Count >= 2) {
                        break;
                    }
                }
            }
            
            foreach (BallController ball in mergeBalls)
            {
                int ballLevel = ball.ballOptions.level;
                Debug.Log("Ball Level: " + ballLevel);
            }
    
            if (mergeBalls.Count < 2)
            {
                Debug.Log("Not enough balls found for merge.");
            }
    
            return mergeBalls;
        }
    }
}