using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

public class UIOptionSelectorController : Singleton<UIOptionSelectorController>
{
    public enum SelectorType
    {
        Action,
        Tech,
        Building
    }

    public SelectorType m_selectorType;

    public GameObject m_actionHolder;
    public GameObject m_techHolder;
    public GameObject m_buildingHolder;

    void Update()
    {
        m_actionHolder.SetActive(m_selectorType == SelectorType.Action);
        m_techHolder.SetActive(m_selectorType == SelectorType.Tech);
        m_buildingHolder.SetActive(m_selectorType == SelectorType.Building);
        
    }
}
