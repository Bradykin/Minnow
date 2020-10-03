using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

public class UIStaminaContainer : MonoBehaviour
{
    public List<UIStaminaBubble> m_StaminaBubbles;
    public bool m_inWorld;

    public void Init(int curStamina, int maxStamina, Team team)
    {
        if (m_StaminaBubbles != null)
        {
            for (int i = m_StaminaBubbles.Count - 1 ; i >= 0; i--)
            {
                Recycler.Recycle<UIStaminaBubble>(m_StaminaBubbles[i]);
            }
        }

        m_StaminaBubbles = new List<UIStaminaBubble>();

        for (int i = 0; i < maxStamina; i++)
        {
            bool isActive = i < curStamina;
            m_StaminaBubbles.Add(FactoryManager.Instance.GetFactory<UIStaminaBubbleFactory>().CreateObject<UIStaminaBubble>(transform, isActive, team, i, m_inWorld));
        }
    }

    public void DoUpdate(int curStamina, int maxStamina, Team team)
    {
        if (maxStamina != m_StaminaBubbles.Count)
        {
            Init(curStamina, maxStamina, team);
            return;
        }
        else
        {
            for (int i = 0; i < m_StaminaBubbles.Count; i++)
            {
                m_StaminaBubbles[i].Init(i < curStamina, team);
            }
        }
    }
}
