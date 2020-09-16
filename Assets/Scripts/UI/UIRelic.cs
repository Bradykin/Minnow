using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRelic : WorldElementBase
{
    public enum RelicSelectionType
    {
        View,
        Select
    }

    private GameRelic m_relic;
    private RelicSelectionType m_selectionType;

    public SpriteRenderer m_renderer;
    public SpriteRenderer m_tintRenderer;

    public void Init(GameRelic newRelic, RelicSelectionType selectionType)
    {
        m_relic = newRelic;
        m_selectionType = selectionType;

        m_renderer.sprite = m_relic.m_icon;
        m_tintRenderer.sprite = m_relic.m_icon;
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
        UIHelper.SetValidTintColor(m_tintRenderer, true);
        Globals.m_canScroll = false;
    }

    void OnMouseExit()
    {
        UIHelper.SetDefaultTintColor(m_tintRenderer);
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
