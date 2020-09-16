using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICardSelectAcceptButton : WorldElementBase
{
    public SpriteRenderer m_tintRenderer;

    public SpriteRenderer m_backgroundRenderer;
    public Text m_acceptText;

    private bool m_isActive;

    void Update()
    {
        m_isActive = Globals.m_selectedCard != null;

        if (m_isActive)
        {
            m_backgroundRenderer.color = UIHelper.m_defaultColor;
            m_acceptText.color = UIHelper.m_defaultColor;
        }
        else
        {
            m_backgroundRenderer.color = UIHelper.m_defaultFaded;
            m_acceptText.color = UIHelper.m_defaultFaded;
        }
    }

    void OnMouseDown()
    {
        if (!m_isActive)
        {
            return;
        }

        UICardSelectController.Instance.AcceptCard(Globals.m_selectedCard.m_card);
        UIHelper.SetDefaultTintColor(m_tintRenderer);
        Globals.m_selectedCard = null;
    }

    void OnMouseOver()
    {
        UIHelper.SetValidTintColor(m_tintRenderer, true);
    }

    void OnMouseExit()
    {
        UIHelper.SetDefaultTintColor(m_tintRenderer);
    }

    public override void HandleTooltip()
    {
        if (!m_isActive)
        {
            UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Select Card", "Need to select a card.", false));
        }
    }
}
