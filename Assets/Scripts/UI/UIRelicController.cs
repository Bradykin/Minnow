using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

public class UIRelicController : Singleton<UIRelicController>
{
    private List<UIRelic> m_relics = new List<UIRelic>();

    public void AddRelic(GameRelic relic)
    {
        m_relics.Add(FactoryManager.Instance.GetFactory<UIRelicFactory>().CreateObject<UIRelic>(relic, transform));
    }

    public void TriggerRelicAnimation<T>()
    {
        for (int i = 0; i < m_relics.Count; i++)
        {
            if (m_relics[i].m_relic is T val)
            {
                m_relics[i].OnTrigger();
            }
        }
    }

    void Update()
    {
        for (int i = 0; i < m_relics.Count; i++)
        {
            m_relics[i].transform.localPosition = new Vector3(200.0f + i * 60.0f, -25.0f, 0.0f);
        }
    }

    public void ClearRelics()
    {
        for (int i = m_relics.Count-1; i >= 0; i--)
        {
            Recycler.Recycle<UIRelic>(m_relics[i]);
        }

        m_relics = new List<UIRelic>();
    }
}
