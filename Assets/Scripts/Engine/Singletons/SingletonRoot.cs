using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Util
{
    public class SingletonRoot : Singleton<SingletonRoot>
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
    }
}