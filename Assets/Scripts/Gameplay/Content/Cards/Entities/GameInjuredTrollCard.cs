using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInjuredTrollCard : GameCardEntityBase
{
    public GameInjuredTrollCard()
    {
        m_entity = new GameInjuredTrollEntity();

        m_name = m_entity.m_name;
        m_desc = m_entity.m_desc;
        m_playDesc = "This troll may be injured for now, but it will soon be full strength and mighty!";
        m_typeline = "Summon - Troll";
        m_cost = 2;
        m_icon = UIHelper.GetIconCard(m_name);
    }
}
