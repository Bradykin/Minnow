using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;
using TMPro;
using UnityEngine.Events;

public class UIConfirmationController : Singleton<UIConfirmationController>
{
    public GameObject m_holder;
    public TMP_Text m_descText;
    public UnityAction m_action;

    public void Init(string descText, UnityAction action)
    {
        m_descText.text = descText;
        m_action = action;
        m_holder.SetActive(true);
    }

    public void Accept()
    {
        m_action();
        m_holder.SetActive(false);
    }

    public void Cancel()
    {
        m_holder.SetActive(false);
    }
}
