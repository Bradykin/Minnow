using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentOrcEnemy : GameEnemyEntity
{
    public ContentOrcEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 10;
        m_maxAP = 8;
        m_apRegen = 4;
        m_power = 6;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Orc";
        m_desc = "Feirce and strong, the backbone of any good army.";

        LateInit();
    }
}