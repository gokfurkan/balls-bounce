using Template.Scripts.Scriptables;
using UnityEngine;

namespace Template.Scripts
{
    public class Pooling : PersistentSingleton<Pooling>
    {
        [SerializeField] private Transform parent;
        public Pool[] poolObjects;

        protected override void Initialize()
        {
            if (poolObjects == null || poolObjects.Length == 0)
            {
                Debug.LogWarning("PoolObjects array is null or empty. Please assign valid objects.");
                return;
            }

            foreach (var poolObject in poolObjects)
            {
                if (poolObject == null)
                {
                    Debug.LogWarning("Skipping null PoolObject. Please assign a valid object.");
                    continue;
                }

                InitializePoolObject(poolObject);
            }
        }

        private void InitializePoolObject(Pool poolObject)
        {
            Pool instantiatedObject = Instantiate(poolObject);
    
            if (instantiatedObject != null)
            {
                instantiatedObject.Setup(parent);
            }
            else
            {
                Debug.LogError("Failed to instantiate PoolObject. Check if the object is assigned in the inspector.");
            }
        }
    }
}