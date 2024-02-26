using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Game.Dev.Scripts.Scriptables;
using MoreMountains.NiceVibrations;
using Sirenix.OdinInspector;
using Template.Scripts;
using UnityEngine;

namespace Game.Dev.Scripts
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
        public ParticleSystem mergeParticle;

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
            StartCoroutine(InitHaveBalls());
        }

        private void AddNewBall(int level)
        {
            var createdBall = BallPooling.instance.poolObjects[level].GetItem();
            createdBall.transform.SetParent(ballHolder , true);

            var createdBallController = createdBall.GetComponent<BallController>();
            balls.Add(createdBallController);
            SetHaveBallLevelList();
            
            SpawnBall(createdBallController);
            BusSystem.CallRefreshUpgradeValues();
        }

        [Button]
        private void MergeBall()
        {
            List<BallController> highestBalls = FindMergeBalls();
            
            if (!CanMerge()) return;
            
            foreach (var ball in highestBalls)
            {
                balls.Remove(ball);
            }
            
            AudioManager.instance.Play(AudioType.MergeStart);
            BusSystem.CallRefreshUpgradeValues();
           
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
                }
                
                mergeParticle.Play();
                AudioManager.instance.Play(AudioType.MergeComplete);
                HapticManager.instance.PlayHaptic(HapticTypes.MediumImpact);
                
                var upgradedBall = BallPooling.instance.poolObjects[highestBalls[0].ballOptions.level + 1].GetItem();
                var upgradedBallController = upgradedBall.GetComponent<BallController>();
                
                balls.Add(upgradedBallController);
                upgradedBall.transform.SetParent(ballHolder, true);
                upgradedBall.transform.position = mergePoints[2].position;
                upgradedBall.gameObject.SetActive(true);

                SetHaveBallLevelList();
                
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

        private IEnumerator InitHaveBalls()
        {
            List<int> haveBallList = SaveManager.instance.saveData.haveBallLevels;

            for (int i = 0; i < haveBallList.Count; i++)
            {
                AddNewBall(haveBallList[i]);
                yield return new WaitForSeconds(0.35f);
            }
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
            }
    
            if (mergeBalls.Count < NEED_MERGE_AMOUNT)
            {
                // Debug.Log("Not enough balls found for merge.");
            }
    
            return mergeBalls;
        }

        public bool CanMerge()
        {
            return FindMergeBalls().Count == NEED_MERGE_AMOUNT;
        }

        private void SetHaveBallLevelList()
        {
            SaveManager.instance.saveData.haveBallLevels.Clear();
            for (int i = 0; i < balls.Count; i++)
            {
                SaveManager.instance.saveData.haveBallLevels.Add(balls[i].ballOptions.level);
            }
        }
    }
}