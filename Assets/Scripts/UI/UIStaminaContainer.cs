using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

public class UIStaminaContainer : MonoBehaviour
{
    public List<UIStaminaBubble> m_staminaBubbles;
    public bool m_inWorld;

    private int m_cachedCurStamina;
    private int m_cachedMaxStamina;
    private Team m_cachedTeam;

    public void Init(int curStamina, int maxStamina, Team team)
    {
        bool canSkip = CanSkip(curStamina, maxStamina, team);
        
        if (canSkip)
        {
            return;
        }

        m_cachedCurStamina = curStamina;
        m_cachedMaxStamina = maxStamina;
        m_cachedTeam = team;

        if (m_staminaBubbles != null)
        {
            for (int i = m_staminaBubbles.Count - 1 ; i >= 0; i--)
            {
                Recycler.Recycle<UIStaminaBubble>(m_staminaBubbles[i]);
            }
        }

        m_staminaBubbles = new List<UIStaminaBubble>();

        for (int i = 0; i < maxStamina; i++)
        {
            bool isActive = i < curStamina;
            m_staminaBubbles.Add(FactoryManager.Instance.GetFactory<UIStaminaBubbleFactory>().CreateObject<UIStaminaBubble>(transform, isActive, team, i, m_inWorld));
        }
    }

    public void DoUpdate(int curStamina, int maxStamina, Team team)
    {
        if (maxStamina != m_staminaBubbles.Count)
        {
            Init(curStamina, maxStamina, team);
            return;
        }
        else
        {
            for (int i = 0; i < m_staminaBubbles.Count; i++)
            {
                m_staminaBubbles[i].Init(i < curStamina, team);
            }
        }
    }

    public bool CanSkip(int curStamina, int maxStamina, Team team)
    {
        if (curStamina != m_cachedCurStamina)
        {
            return false;
        }

        if (maxStamina != m_cachedMaxStamina)
        {
            return false;
        }

        if (team != m_cachedTeam)
        {
            return false;
        }

        return true;
    }
}
