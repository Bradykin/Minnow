using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Util;

public class UIEventController : Singleton<UIEventController>
{
    public Text m_titleText;
    public Text m_descText;

    public GameObject m_holder;

    public UIEventButton m_firstButton;
    public UIEventButton m_secondButton;
    public UIEventButton m_thirdButton;

    private GameEvent m_event;

    void Start()
    {
        m_holder.SetActive(false);
    }

    public void Init(GameEvent gameEvent)
    {
        Globals.m_canSelect = false;
        Globals.m_selectedCard = null;
        Globals.m_selectedEntity = null;

        m_event = gameEvent;

        m_titleText.text = m_event.m_name;
        m_descText.text = m_event.m_eventDesc;

        m_firstButton.Init(m_event.m_optionOne);
        m_secondButton.Init(m_event.m_optionTwo);
        m_thirdButton.Init(m_event.m_optionThree);

        m_holder.SetActive(true);
    }

    public void EndEvent()
    {
        Globals.m_canSelect = true;
        m_event.m_isComplete = true;
        m_holder.SetActive(false);
    }
}
