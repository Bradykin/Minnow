using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct JsonGameEntityData
{
    //GameElementBase values
    public string m_name;
    public string m_desc;
    public Sprite m_icon;
    public int m_rarity;
    public Color m_color;

    //GameEntity data
    public int m_team;
    public int m_curHealth;
    public int m_maxHealth;
    public int m_curAP;
    public int m_apRegen;
    public int m_maxAP;
    public int m_power;
    public int m_typeline;
    public GameKeywordHolder m_keywordHolder;
    public int m_apToAttack;
    public int m_sightRange;
}
