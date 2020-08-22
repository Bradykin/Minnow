using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRelic : WorldElementBase
{
    private GameRelic m_relic;

    public SpriteRenderer m_renderer;
    public SpriteRenderer m_tintRenderer;

    public void Init(GameRelic newRelic)
    {
        m_relic = newRelic;

        m_renderer.sprite = m_relic.m_icon;
    }

    public override void HandleTooltip()
    {
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip(m_relic.m_name, m_relic.m_desc));
    }
}
