using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

public class UIWorldElementNotificationController : Singleton<UIWorldElementNotificationController>
{
    private List<string> m_messages = new List<string>();
    private List<Color> m_colors = new List<Color>();
    private List<GameObject> m_positionObjs = new List<GameObject>();

    private int m_counter;
    private const int m_counterLimit = 60;

    void FixedUpdate()
    {
        int numWaiting = m_messages.Count;

        m_counter++;

        if (numWaiting > 0 && m_counter >= m_counterLimit)
        {
            RaiseMessage();
        }
    }

    public void AddWorldElementNotification(string message, Color color, GameObject positionObj)
    {
        m_messages.Add(message);
        m_colors.Add(color);
        m_positionObjs.Add(positionObj);
    }

    private void RaiseMessage()
    {
        FactoryManager.Instance.GetFactory<UIWorldElementNotificationFactory>().CreateObject<UIWorldElementNotification>(m_messages[0], m_colors[0], m_positionObjs[0]);
        m_messages.RemoveAt(0);
        m_colors.RemoveAt(0);
        m_positionObjs.RemoveAt(0);

        m_counter = 0;
    }
}
