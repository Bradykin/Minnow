using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Util
{
    /// <summary>
    /// Used for Scene Dependent Singletons (Destoy Object on Load)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonDontDestroyOnLoad<T> : MonoBehaviour where T: Object
    {
        public static T Instance => _instance;
        private static T _instance;
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
    }
}

