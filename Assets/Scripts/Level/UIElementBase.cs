using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UIElementBase : MonoBehaviour
     , IPointerEnterHandler
     , IPointerExitHandler
{
    protected GameElementBase m_gameElement;
    protected bool m_isShowingTooltip;
    protected bool m_stopScrolling;

    public bool m_showTooltip { get; protected set; } = true;

    public Image m_tintImage;

    public virtual GameElementBase GetElement()
    {
        return m_gameElement;
    }

    public abstract void HandleTooltip();

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        m_tintImage.color = UIHelper.GetValidTintColor(true);

        if (!m_isShowingTooltip)
        {
            HandleTooltip();

            m_isShowingTooltip = true;
        }

        if (m_stopScrolling)
        {
            Globals.m_canScroll = false;
        }
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        m_tintImage.color = UIHelper.GetDefaultTintColor();

        ClearTooltip();

        if (m_stopScrolling)
        {
            Globals.m_canScroll = true;
        }
    }

    public void ClearTooltip()
    {
        UITooltipController.Instance.ClearTooltipStack();

        m_isShowingTooltip = false;
    }
}
