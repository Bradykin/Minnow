using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICardSelectAcceptButton : WorldElementBase
{
    public Image m_tintImage;

    public Image m_image;
    public Text m_acceptText;

    private bool m_isActive;

    void Update()
    {
        m_isActive = Globals.m_selectedCard != null;

        if (m_isActive)
        {
            m_image.color = UIHelper.m_defaultColor;
            m_acceptText.color = UIHelper.m_defaultColor;
        }
        else
        {
            m_image.color = UIHelper.m_defaultFaded;
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
        m_tintImage.color = UIHelper.GetDefaultTintColor();
        Globals.m_selectedCard = null;
    }

    void OnMouseOver()
    {
        m_tintImage.color = UIHelper.GetValidTintColor(true);
    }

    void OnMouseExit()
    {
        m_tintImage.color = UIHelper.GetDefaultTintColor();
    }

    public override void HandleTooltip()
    {
        if (!m_isActive)
        {
            UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Select Card", "Need to select a card.", false));
        }
    }
}
