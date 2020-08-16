using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITooltipGenerator : MonoBehaviour
{
    public GameElementBase m_element;

    void OnMouseOver()
    {
        WorldElementBase worldElement = GetComponent<WorldElementBase>();
        m_element = worldElement.GetElement();
        if (m_element != null && worldElement.m_showTooltip)
        {
            m_element.InitTooltip();
        }
    }

    void OnMouseExit()
    {
        if (m_element != null)
        {
            m_element.ClearTooltip();
        }
    }
}
