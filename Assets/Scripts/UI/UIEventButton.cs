using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEventButton : MonoBehaviour
{
    public GameEventOption m_eventOption { get; private set; }
    public Text m_descText;

    public void Init(GameEventOption gameEventOption)
    {
        m_eventOption = gameEventOption;

        m_descText.text = m_eventOption.m_message;
    }

    void OnMouseDown()
    {
        m_eventOption.AcceptOption();
    }
}
