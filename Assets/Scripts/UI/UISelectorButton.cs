using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UISelectorButton : WorldElementBase
    , IPointerClickHandler
{
    public Image m_image;

    public UIIntermissionController.SelectorType m_selectorType;

    void Start()
    {
        m_stopScrolling = true;
    }

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

    public void OnPointerClick(PointerEventData eventData)
    {
        UIIntermissionController.Instance.SetSelectorType(m_selectorType);
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
