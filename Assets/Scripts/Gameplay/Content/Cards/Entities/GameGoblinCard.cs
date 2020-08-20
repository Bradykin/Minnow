using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGoblinCard : GameCardEntityBase
{
    public GameGoblinCard()
    {
        m_entity = new GameGoblinEntity();

        m_name = m_entity.m_name;
        m_desc = m_entity.m_desc;
        m_playDesc = "A dubious goblin joins the fray on your side!  Yay...";
        m_typeline = "Summon - Goblin";
        m_cost = 1;
        m_icon = UIHelper.GetIconCard(m_name);
    }
}
