using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;
using TMPro;

public class UINewVersionNotificationController : Singleton<UINewVersionNotificationController>
{
    public GameObject m_holder;
    public TMP_Text m_title;
    public TMP_Text m_desc;

    public void Init()
    {
        m_holder.SetActive(true);

        m_title.text = "New Version";
        m_desc.text = "The intermission actions have been overhauled!  As a result of this; in-progress run save data has been cleared. Unlocks should be unchanged.";
    }

    public void Close()
    {
        m_holder.SetActive(false);
    }
}
