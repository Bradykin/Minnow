using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

public class UIRelicController : Singleton<UIRelicController>
{
    private List<UIRelic> m_relics;

    void Start()
    {
        m_relics = new List<UIRelic>();    
    }

    public void AddRelic(GameRelic relic)
    {
        m_relics.Add(FactoryManager.Instance.GetFactory<UIRelicFactory>().CreateObject<UIRelic>(relic, transform));
    }

    void Update()
    {
        for (int i = 0; i < m_relics.Count; i++)
        {
            m_relics[i].transform.localPosition = new Vector3(150.0f + i * 75.0f, -50.0f, 0.0f);
        }
    }
}
