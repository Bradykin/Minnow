using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDwarvenSoldier : GameEntity
{
    public ContentDwarvenSoldier()
    {
        m_maxHealth = 15;
        m_maxAP = 4;
        m_apRegen = 2;
        m_power = 5;

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        m_name = "Dwarven Soldier";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }
}
