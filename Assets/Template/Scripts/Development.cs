using Template.Scripts;
using UnityEngine;

namespace Game.Dev.Scripts
{
    public class Development : PersistentSingleton<Development>
    {
#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                Debug.Break();
            }
            
            if (Input.GetKeyDown(KeyCode.G))
            {
                BusSystem.CallLevelEnd(true);
            }
            
            if (Input.GetKeyDown(KeyCode.H))
            {
                BusSystem.CallLevelEnd(false);
            }
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                BusSystem.CallLevelLoad();
            }
            
            if (Input.GetKeyDown(KeyCode.M))
            {
                BusSystem.CallAddMoneys(50000);
            }
            
            if (Input.GetKeyDown(KeyCode.P))
            {
                BusSystem.CallAddNewBall(0);
            }
        }
#endif
    }
}