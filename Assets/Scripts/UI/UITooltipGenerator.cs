using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITooltipGenerator : MonoBehaviour
{
    private WorldElementBase m_worldElement;

    private bool m_isShowingTooltip;

    void Start()
    {
        m_worldElement = GetComponent<WorldElementBase>();
    }

    void OnMouseOver()
    {
        if (!m_isShowingTooltip)
        {
            m_worldElement.HandleTooltip();

            m_isShowingTooltip = true;
        }
    }

    void OnMouseExit()
    {
        ClearTooltip();
    }

    public void ClearTooltip()
    {
        UITooltipController.Instance.ClearTooltipStack();

        m_isShowingTooltip = false;
    }
}
