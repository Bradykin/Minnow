using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentNaturalScout : GameEntity
{
    public ContentNaturalScout()
    {
        m_maxHealth = 1;
        m_maxAP = 10;
        m_apRegen = 5;
        m_power = 1;
        m_apToAttack = 5;
        m_sightRange = 4;

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        m_name = "Natural Scout";
        m_desc = "Takes 5 AP to attack.";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }
}
