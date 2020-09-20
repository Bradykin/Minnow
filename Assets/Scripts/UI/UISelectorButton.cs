using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelectorButton : WorldElementBase
{
    public Image m_image;
    public Image m_tintImage;

    public UIIntermissionController.SelectorType m_selectorType;

    void Update()
    {
        if (m_selectorType == UIIntermissionController.Instance.GetSelectorType())
        {
            m_image.color = Color.yellow;
        }
        else
        {
            m_image.color = Color.white;
        }
    }

    void OnMouseDown()
    {
        UIIntermissionController.Instance.SetSelectorType(m_selectorType);
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
