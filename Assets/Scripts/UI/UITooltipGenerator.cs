using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITooltipGenerator : MonoBehaviour
{
    public GameElementBase m_element;

    void OnMouseOver()
    {
        m_element = GetComponent<WorldElementBase>().GetElement();
        if (m_element != null)
        {
            m_element.InitTooltip();
            print("Entering!" + m_element.m_name);
        }
    }

    void OnMouseExit()
    {
        if (m_element != null)
        {
            m_element.ClearTooltip();
            print("Exiting!");
        }
    }
}
