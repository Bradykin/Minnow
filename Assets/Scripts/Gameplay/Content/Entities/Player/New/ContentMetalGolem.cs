using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMetalGolem : GameEntity
{
    public ContentMetalGolem()
    {
        m_maxHealth = 8;
        m_maxAP = 4;
        m_apRegen = 3;
        m_power = 4;

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        m_name = "Metal Golem";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }
}