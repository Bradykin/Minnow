using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct JsonGameEventData
{
    //GameElementBase values
    public string m_name;
    public string m_desc;
    public Sprite m_icon;
    public int m_rarity;
    public Color m_color;

    //GameEvent values
    public int m_APCost;
    public string m_eventDesc;
    public bool m_isComplete;
}
