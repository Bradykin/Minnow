using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Util;
using TMPro;

public class UIEventController : Singleton<UIEventController>
{
    public TMP_Text m_titleText;
    public TMP_Text m_descText;

    public GameObject m_holder;

    public UIEventButton m_firstButton;
    public UIEventButton m_secondButton;

    private GameEvent m_event;

    void Start()
    {
        m_holder.SetActive(false);
    }

    public void Init(GameEvent gameEvent)
    {
        gameEvent.LateInit();

        Globals.m_canSelect = false;
        UIHelper.UnselectUnit();

        m_event = gameEvent;

        m_titleText.text = m_event.GetName();
        m_descText.text = m_event.m_eventDesc;

        if (m_event.m_optionOne != null)
        {
            m_firstButton.gameObject.SetActive(true);
            m_firstButton.Init(m_event.m_optionOne);
        }
        else
        {
            m_firstButton.gameObject.SetActive(false);
        }

        if (m_event.m_optionTwo != null)
        {
            m_secondButton.gameObject.SetActive(true);
            m_secondButton.Init(m_event.m_optionTwo);
        }
        else
        {
            m_secondButton.gameObject.SetActive(false);
        }

        m_holder.SetActive(true);
    }

    public void EndEvent()
    {
        Globals.m_canSelect = true;
        m_event.m_isComplete = true;
        m_holder.SetActive(false);
    }
}
