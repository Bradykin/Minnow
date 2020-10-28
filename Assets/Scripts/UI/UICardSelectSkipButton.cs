using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UICardSelectSkipButton : UIElementBase
    , IPointerClickHandler
{
    public Image m_image;
    public Text m_skipText;

    private bool m_isActive;

    void Update()
    {
        m_isActive = Globals.m_selectedCard == null;

        if (m_isActive)
        {
            m_image.color = UIHelper.m_defaultColor;
            m_skipText.color = UIHelper.m_defaultColor;
        }
        else
        {
            m_image.color = UIHelper.m_defaultFaded;
            m_skipText.color = UIHelper.m_defaultFaded;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioSFXController.Instance.PlaySFX(AudioHelper.UIClick);

        if (!m_isActive)
        {
            return;
        }

        UICardSelectController.Instance.SkipSelection();
        m_tintImage.color = UIHelper.GetDefaultTintColor();
        Globals.m_selectedCard = null;
    }

    public override void HandleTooltip()
    {
        if (!m_isActive)
        {
            UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Skip Card", "You have a card selected", false));
        }
    }
}
