using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;
using UnityEngine.UI;

public class UIMapEventController : Singleton<UIMapEventController>
{
    private List<GameMapEvent> m_mapEventQueue = new List<GameMapEvent>();

    public GameObject m_holder;
    public Text m_titleText;
    public Text m_descText;

    public void AddEvent(GameMapEvent newMapEvent)
    {
        m_holder.SetActive(true);

        m_mapEventQueue.Add(newMapEvent);
    }

    void Update()
    {
        if (m_mapEventQueue.Count > 0)
        {
            m_titleText.text = m_mapEventQueue[0].m_name;
            m_descText.text = m_mapEventQueue[0].m_desc;
        }
    }

    public void CloseCurrentEvent()
    {
        if (m_mapEventQueue.Count == 0)
        {
            return;
        }

        m_mapEventQueue.RemoveAt(0);

        if (m_mapEventQueue.Count == 0)
        {
            m_holder.SetActive(false);
        }
    }
}
