using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelectorButton : WorldElementBase
{
    public SpriteRenderer m_renderer;
    public SpriteRenderer m_tintRenderer;

    public UIOptionSelectorController.SelectorType m_selectorType;

    void Update()
    {
        if (m_selectorType == UIOptionSelectorController.Instance.GetSelectorType())
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
        UIOptionSelectorController.Instance.SetSelectorType(m_selectorType);
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
        if (m_selectorType == UIOptionSelectorController.SelectorType.Action)
        {
            UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Actions", "These actions don't cost resources, but they do still take action points."));
        }
        else if (m_selectorType == UIOptionSelectorController.SelectorType.Tech)
        {
            UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Tech", "While expensive, these are upgrades that will persist the rest of the run."));
        }
        else if (m_selectorType == UIOptionSelectorController.SelectorType.Building)
        {
            UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Buildings", "Place these in the world to either help with defense, or to help generate resources.  If one is destroyed in a wave, it will come back during the next intermission."));
        }
    }
}
