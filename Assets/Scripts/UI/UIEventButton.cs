using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEventButton : MonoBehaviour
{
    public SpriteRenderer m_tintRenderer;
    public GameEventOption m_eventOption { get; private set; }
    public Text m_descText;

    public void Init(GameEventOption gameEventOption)
    {
        m_eventOption = gameEventOption;

        m_descText.text = m_eventOption.m_message;
    }

    void OnMouseDown()
    {
        if (m_eventOption == null)
        {
            return;
        }

        m_eventOption.AcceptOption();
    }

    void OnMouseOver()
    {
        if (m_eventOption == null)
        {
            return;
        }

        UIHelper.SetValidTintColor(m_tintRenderer, m_eventOption.IsOptionValid());
    }

    void OnMouseExit()
    {
        UIHelper.SetDefaultTintColor(m_tintRenderer);
    }
}
