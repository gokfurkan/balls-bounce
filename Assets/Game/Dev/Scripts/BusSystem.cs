using System;
using Game.Dev.Scripts.Ball;
using UnityEngine;

namespace Game.Dev.Scripts
{
    public static class BusSystem
    {
        //Economy
        public static Action <int> OnAddMoneys;
        public static void CallAddMoneys(int amount) { OnAddMoneys?.Invoke(amount); }
        
        public static Action OnResetMoneys;
        public static void CallResetMoneys() { OnResetMoneys?.Invoke(); }
        
        public static Action OnSetMoneys;
        public static void CallSetMoneys() { OnSetMoneys?.Invoke(); }
        
        public static Action OnSpawnMoneys;
        public static void CallSpawnMoneys() { OnSpawnMoneys?.Invoke(); }
        
        public static Action <Transform , int> OnSpawnIncomeVisual;
        public static void CallSpawnIncomeVisual(Transform pos , int incomeAmount) { OnSpawnIncomeVisual?.Invoke(pos , incomeAmount); }
        
        //Game Manager
        
        public static Action OnLevelStart;
        public static void CallLevelStart() { OnLevelStart?.Invoke(); }
     
        public static Action <bool> OnLevelEnd;
        public static void CallLevelEnd(bool win) { OnLevelEnd?.Invoke(win); }
     
        public static Action OnLevelLoad;
        public static void CallLevelLoad() { OnLevelLoad?.Invoke(); }
        
        //Input
        
        public static Action OnMouseClickDown;
        public static void CallMouseClickDown() { OnMouseClickDown?.Invoke(); }
        
        public static Action OnMouseClicking;
        public static void CallMouseClicking() { OnMouseClicking?.Invoke(); }
        
        public static Action OnMouseClickUp;
        public static void CallMouseClickUp() { OnMouseClickUp?.Invoke(); }
        
        //Upgrades
        
        public static Action OnRefreshUpgradeValues;
        public static void CallRefreshUpgradeValues() { OnRefreshUpgradeValues?.Invoke(); }
        
        public static Action <int> OnAddNewBall;
        public static void CallAddNewBall(int level) { OnAddNewBall?.Invoke(level); }
        
        public static Action OnMergeBall;
        public static void CallMergeBall() { OnMergeBall?.Invoke(); }
        
        public static Action <BallController> OnReSpawnBall;
        public static void CallReSpawnBall(BallController ball) { OnReSpawnBall?.Invoke(ball); }
    }
}