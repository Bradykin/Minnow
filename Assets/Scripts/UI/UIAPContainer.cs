using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

public class UIAPContainer : MonoBehaviour
{
    public List<UIAPBubble> m_APBubbles;

    public void Init(int curAP, int maxAP)
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
            FactoryManager.Instance.GetFactory<UIAPBubbleFactory>().CreateObject<UIAPBubble>(transform, isActive, i);
        }

        for (int i = 0; i < curAP; i++)
        {

        }
    }
}
