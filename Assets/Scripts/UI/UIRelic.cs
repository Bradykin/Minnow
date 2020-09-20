using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIRelic : WorldElementBase
    , IPointerClickHandler
{
    public enum RelicSelectionType
    {
        View,
        Select
    }

    private GameRelic m_relic;
    private RelicSelectionType m_selectionType;

    public Image m_image;

    public void Init(GameRelic newRelic, RelicSelectionType selectionType)
    {
        m_stopScrolling = true;

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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (m_selectionType == RelicSelectionType.Select)
        {
            UIRelicSelectController.Instance.AcceptRelic(m_relic);
            UITooltipController.Instance.ClearTooltipStack();
        }
    }
}
