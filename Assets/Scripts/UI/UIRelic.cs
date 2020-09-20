using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRelic : WorldElementBase
{
    public enum RelicSelectionType
    {
        View,
        Select
    }

    private GameRelic m_relic;
    private RelicSelectionType m_selectionType;

    public Image m_image;
    public Image m_tintImage;

    public void Init(GameRelic newRelic, RelicSelectionType selectionType)
    {
        m_relic = newRelic;
        m_selectionType = selectionType;

        m_image.sprite = m_relic.m_icon;
        m_tintImage.sprite = m_relic.m_icon;
    }

    public override void HandleTooltip()
    {
        if (m_selectionType == RelicSelectionType.View)
        {
            UIHelper.CreateRelicTooltip(m_relic);
        }
    }

    void OnMouseOver()
    {
        m_tintImage.color = UIHelper.GetValidTintColor(true);
        Globals.m_canScroll = false;
    }

    void OnMouseExit()
    {
        m_tintImage.color = UIHelper.GetDefaultTintColor();
        Globals.m_canScroll = true;
    }

    void OnMouseDown()
    {
        if (m_selectionType == RelicSelectionType.Select)
        {
            UIRelicSelectController.Instance.AcceptRelic(m_relic);
            UITooltipController.Instance.ClearTooltipStack();
        }
    }
}
