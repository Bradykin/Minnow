using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCaveDragonCard : GameCardEntityBase
{
    public GameCaveDragonCard()
    {
        m_entity = new GameCaveDragonEntity();

        m_name = m_entity.m_name;
        m_desc = m_entity.m_desc;
        m_playDesc = "While stunted by growing in a cave; this dragon is still feirce!";
        m_typeline = "Summon - Dragon";
        m_cost = 4;
        m_icon = UIHelper.GetIconCard(m_name);
    }
}
