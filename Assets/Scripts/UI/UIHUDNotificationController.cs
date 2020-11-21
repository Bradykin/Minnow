using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;
using UnityEngine.UI;
using TMPro;

public class UIHUDNotificationController : Singleton<UIHUDNotificationController>
{
    public GameObject m_moreNotificationIndicator;

    private List<string> m_titles = new List<string>();
    private List<string> m_descs = new List<string>();

    public GameObject m_holder;
    public TMP_Text m_titleText;
    public TMP_Text m_descText;

    public void AddNotification(string name, string desc)
    {
        m_holder.SetActive(true);

        m_titles.Add(name);
        m_descs.Add(desc);
    }

    void Update()
    {
        if (m_titles.Count > 0)
        {
            m_titleText.text = m_titles[0];
            m_descText.text = m_descs[0];

            m_moreNotificationIndicator.SetActive(m_titles.Count > 1);
        }
    }

    public void CloseCurrentEvent()
    {
        if (m_titles.Count == 0)
        {
            return;
        }

        m_titles.RemoveAt(0);
        m_descs.RemoveAt(0);

        if (m_titles.Count == 0)
        {
            m_holder.SetActive(false);
        }
    }
}
