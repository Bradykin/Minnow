using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Util
{
    public class Timer : Singleton<Timer>
    {
        private float m_levelTime;

        void Update()
        {
            m_levelTime += Time.deltaTime;
        }

        public void ResetLevelTime()
        {
            m_levelTime = 0.0f;
        }

        public float GetLevelTime()
        {
            return m_levelTime;
        }

        public void SetLevelTime(float levelTime)
        {
            m_levelTime = levelTime;
        }
    }
}
