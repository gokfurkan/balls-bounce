using UnityEngine;

namespace Template.Scripts
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        public static T instance { get; private set; }
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this);
            }
            else
            {
                instance = this as T;
                Initialize();
            }
        }

        protected virtual void Initialize()
        {
        }
    }

    public class PersistentSingleton<T> : MonoBehaviour where T : Component
    {
        public static T instance { get; private set; }
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this);
            }
            else
            {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
                Initialize();
            }
        }

        protected virtual void Initialize()
        {
        }
    }
}