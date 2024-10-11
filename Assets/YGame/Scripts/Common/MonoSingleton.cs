using UnityEngine;

namespace YGame.Scripts.Common
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));
                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = typeof(T).Name + " (Singleton)";
                    }
                    DontDestroyOnLoad(_instance);
                }
                return _instance;
            }
        }
    }
}