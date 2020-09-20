using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIEventButton : WorldElementBase
    , IPointerClickHandler
{
    public GameEventOption m_eventOption { get; private set; }
    public Text m_descText;

    public void Init(GameEventOption gameEventOption)
    {
        m_eventOption = gameEventOption;

        m_descText.text = m_eventOption.GetMessage();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (m_eventOption == null)
        {
            return;
        }

        m_eventOption.AcceptOption();

        m_tintImage.color = UIHelper.GetDefaultTintColor();
        ClearTooltip();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        if (m_eventOption == null)
        {
            return;
        }

        m_tintImage.color = UIHelper.GetValidTintColor(m_eventOption.IsOptionValid());
    }

    public override void HandleTooltip()
    {
        if (m_eventOption == null)
        {
            return;
        }

        if (m_eventOption.m_hasTooltip)
        {
            m_eventOption.BuildTooltip();
        }
    }
}
