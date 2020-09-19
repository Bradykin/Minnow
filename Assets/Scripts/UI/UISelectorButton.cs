using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelectorButton : WorldElementBase
{
    public SpriteRenderer m_renderer;
    public SpriteRenderer m_tintRenderer;

    public UIIntermissionController.SelectorType m_selectorType;

    void Update()
    {
        if (m_selectorType == UIIntermissionController.Instance.GetSelectorType())
        {
            m_renderer.color = Color.yellow;
        }
        else
        {
            m_renderer.color = Color.white;
        }
    }

    void OnMouseDown()
    {
        UIIntermissionController.Instance.SetSelectorType(m_selectorType);
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

    public override void HandleTooltip()
    {
        if (m_selectorType == UIIntermissionController.SelectorType.Action)
        {
            UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Actions", "These actions don't cost resources, but they do still take action points."));
        }
        else if (m_selectorType == UIIntermissionController.SelectorType.Building)
        {
            UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Buildings", "Place these in the world to either help with defense, or to help generate resources.  If one is destroyed in a wave, it will come back during the next intermission."));
        }
    }
}
