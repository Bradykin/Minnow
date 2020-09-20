using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEventButton : WorldElementBase
{
    public Image m_tintImage;
    public GameEventOption m_eventOption { get; private set; }
    public Text m_descText;

    public void Init(GameEventOption gameEventOption)
    {
        m_eventOption = gameEventOption;

        m_descText.text = m_eventOption.GetMessage();
    }

    void OnMouseDown()
    {
        if (m_eventOption == null)
        {
            return;
        }

        m_eventOption.AcceptOption();

        m_tintImage.color = UIHelper.GetDefaultTintColor();
        GetComponent<UITooltipGenerator>().ClearTooltip();
    }

    void OnMouseOver()
    {
        if (m_eventOption == null)
        {
            return;
        }

        m_tintImage.color = UIHelper.GetValidTintColor(m_eventOption.IsOptionValid());
    }

    void OnMouseExit()
    {
        m_tintImage.color = UIHelper.GetDefaultTintColor();
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
