using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Game.Dev.Scripts.Scriptables;
using Sirenix.OdinInspector;
using Template.Scripts;
using UnityEngine;

namespace Game.Dev.Scripts.Ball
{
    public class BallManager : Singleton<BallManager>
    {
        public List<Transform> mergePoints;
        // public List<BallOptions> ballOptions;
        
        [Space(10)]
        [ReadOnly] public List<BallController> balls;

        [Space(10)] 
        public Transform spawnPos;
        public Transform ballHolder;

        private const int NEED_MERGE_AMOUNT = 2;
        private GameSettings gameSettings;
        
        private void OnEnable()
        {
            BusSystem.OnAddNewBall += AddNewBall;
            BusSystem.OnMergeBall += MergeBall;
            BusSystem.OnReSpawnBall += ReSpawnBall;
        }

        private void OnDisable()
        {
            BusSystem.OnAddNewBall -= AddNewBall;
            BusSystem.OnMergeBall -= MergeBall;
            BusSystem.OnReSpawnBall -= ReSpawnBall;
        }
        
        private void Start()
        {
            gameSettings = InitializeManager.instance.gameSettings;
        }

        private void AddNewBall(int level)
        {
            var createdBall = BallPooling.instance.poolObjects[level].GetItem();
            createdBall.transform.SetParent(ballHolder , true);

            var createdBallController = createdBall.GetComponent<BallController>();
            balls.Add(createdBallController);
            
            SpawnBall(createdBallController);
        }

        [Button]
        private void MergeBall()
        {
            List<BallController> highestBalls = FindMergeBalls();
            
            if (highestBalls.Count < NEED_MERGE_AMOUNT) return;
           
            for (int i = 0; i < NEED_MERGE_AMOUNT; i++)
            {
                highestBalls[i].OnInitMerge();
            }

            int completedMoveToPointTweenCount = 0;

            for (int i = 0; i < NEED_MERGE_AMOUNT; i++)
            {
                Tween moveTween = highestBalls[i].transform.DOMove(mergePoints[i].position, gameSettings.ballManagerOptions.moveToMergePointDuration);
                moveTween.OnComplete(OnMoveToMergePointCompleteOnce);
            }

            void OnMoveToMergePointCompleteOnce()
            {
                completedMoveToPointTweenCount++;
                if (completedMoveToPointTweenCount >= NEED_MERGE_AMOUNT)
                {
                    OnMoveToMergePointComplete();
                }
            }
            
            void OnMoveToMergePointComplete()
            {
                int completedMoveToMergeTweenCount = 0;

                for (int i = 0; i < NEED_MERGE_AMOUNT; i++)
                {
                    Tween moveTween = highestBalls[i].transform.DOMove(mergePoints[2].position, gameSettings.ballManagerOptions.mergeMoveDuration);
                    moveTween.OnComplete(OnMoveToMergeCompleteOnce);
                }

                void OnMoveToMergeCompleteOnce()
                {
                    completedMoveToMergeTweenCount++;
                    if (completedMoveToMergeTweenCount >= NEED_MERGE_AMOUNT)
                    {
                        OnMoveToMergeComplete();
                    }
                }
            }

            void OnMoveToMergeComplete()
            {
                foreach (var ball in highestBalls)
                {
                    BallPooling.instance.poolObjects[ball.ballOptions.level].PutItem(ball.gameObject);
                    balls.Remove(ball);
                }
                
                var upgradedBall = BallPooling.instance.poolObjects[highestBalls[0].ballOptions.level + 1].GetItem();
                var upgradedBallController = upgradedBall.GetComponent<BallController>();
                
                balls.Add(upgradedBallController);
                upgradedBall.transform.SetParent(ballHolder, true);
                upgradedBall.transform.position = mergePoints[2].position;
                upgradedBall.gameObject.SetActive(true);
                
                Tween moveTween = upgradedBall.transform.DOMove(mergePoints[3].position, gameSettings.ballManagerOptions.afterMergeMoveUpDuration);
                moveTween.OnComplete(() => SpawnBall(upgradedBall.GetComponent<BallController>()));
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
                if (levelCounts[level] >= NEED_MERGE_AMOUNT)
                {
                    foreach (BallController ball in balls)
                    {
                        if (ball.ballOptions.level == level)
                        {
                            mergeBalls.Add(ball);
                            if (mergeBalls.Count >= NEED_MERGE_AMOUNT)
                            {
                                break;
                            }
                        }
                    }
                    if (mergeBalls.Count >= NEED_MERGE_AMOUNT) {
                        break;
                    }
                }
            }
            
            foreach (BallController ball in mergeBalls)
            {
                int ballLevel = ball.ballOptions.level;
                Debug.Log("Ball Level: " + ballLevel);
            }
    
            if (mergeBalls.Count < NEED_MERGE_AMOUNT)
            {
                Debug.Log("Not enough balls found for merge.");
            }
    
            return mergeBalls;
        }
    }
}