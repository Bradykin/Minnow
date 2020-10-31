using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

//Need to use a different counter on each gameobject
public class UIWorldElementNotificationController : Singleton<UIWorldElementNotificationController>
{
    private List<string> m_messages = new List<string>();
    private List<Color> m_colors = new List<Color>();
    private List<GameObject> m_positionObjs = new List<GameObject>();

    private const int m_counterLimit = 60;

    private List<GameObject> m_runningNotificationObjs = new List<GameObject>();
    private List<int> m_counters = new List<int>();

    private List<UIWorldElementNotification> m_runningUIElements = new List<UIWorldElementNotification>();

    void FixedUpdate()
    {
        int numWaiting = m_messages.Count;

        for (int i = m_runningNotificationObjs.Count - 1; i >= 0; i--)
        {
            if (m_runningNotificationObjs[i] == null || m_runningNotificationObjs[i].activeSelf == false)
            {
                m_counters.RemoveAt(i);
                m_runningNotificationObjs.RemoveAt(i);
                m_runningUIElements.RemoveAt(i);
                continue;
            }

            m_counters[i]++;

            if (m_counters[i] >= m_counterLimit)
            {
                bool didFind = TryPlayWorldNotification(m_runningNotificationObjs[i]);

                if (didFind)
                {
                    m_counters[i] = 0;
                }
                else
                {
                    m_counters.RemoveAt(i);
                    m_runningNotificationObjs.RemoveAt(i);
                }
            }
        }
    }

    private bool TryPlayWorldNotification(GameObject testObj)
    {
        for (int i = 0; i < m_positionObjs.Count; i++)
        {
            if (testObj == m_positionObjs[i])
            {
                RaiseMessage(i);
                return true;
            }
        }

        return false;
    }

    public void AddWorldElementNotification(string message, Color color, GameObject positionObj)
    {
        m_messages.Add(message);
        m_colors.Add(color);
        m_positionObjs.Add(positionObj);

        if (!m_runningNotificationObjs.Contains(positionObj))
        {
            m_runningNotificationObjs.Add(positionObj);
            m_counters.Add(m_counterLimit);
        }
    }

    private void RaiseMessage(int index)
    {
        UIWorldElementNotification elementNotification = FactoryManager.Instance.GetFactory<UIWorldElementNotificationFactory>().CreateObject<UIWorldElementNotification>(m_messages[index], m_colors[index], m_positionObjs[index]);

        m_runningUIElements.Add(elementNotification);

        m_messages.RemoveAt(index);
        m_colors.RemoveAt(index);
        m_positionObjs.RemoveAt(index);
    }

    public void ClearAllWorldElementNotifications()
    {
        m_messages = new List<string>();
        m_colors = new List<Color>();

        for (int i = m_runningUIElements.Count - 1; i >= 0; i--)
        {
            Destroy(m_runningUIElements[i].gameObject);
        }

        m_runningUIElements = new List<UIWorldElementNotification>();

        m_positionObjs = new List<GameObject>();

        m_runningNotificationObjs = new List<GameObject>();
        m_counters = new List<int>();
    }
}
