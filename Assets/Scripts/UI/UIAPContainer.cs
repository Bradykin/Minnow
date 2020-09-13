using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

public class UIAPContainer : MonoBehaviour
{
    public List<UIAPBubble> m_APBubbles;
    public bool m_inWorld;

    public void Init(int curAP, int maxAP, Team team)
    {
        if (m_APBubbles != null)
        {
            for (int i = m_APBubbles.Count - 1 ; i >= 0; i--)
            {
                Recycler.Recycle<UIAPBubble>(m_APBubbles[i]);
            }
        }

        m_APBubbles = new List<UIAPBubble>();

        for (int i = 0; i < maxAP; i++)
        {
            bool isActive = i < curAP;
            m_APBubbles.Add(FactoryManager.Instance.GetFactory<UIAPBubbleFactory>().CreateObject<UIAPBubble>(transform, isActive, team, i, m_inWorld));
        }
    }

    public void DoUpdate(int curAP, int maxAP, Team team)
    {
        if (maxAP != m_APBubbles.Count)
        {
            Init(curAP, maxAP, team);
            return;
        }
        else
        {
            for (int i = 0; i < m_APBubbles.Count; i++)
            {
                m_APBubbles[i].Init(i < curAP, team);
            }
        }
    }
}
