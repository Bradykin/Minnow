using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLizardmanEnemy : GameEnemyEntity
{
    public ContentLizardmanEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 4;
        m_maxAP = 4;
        m_apRegen = 2;
        m_power = 1;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Lizardman";
        m_desc = "LIZARDMAN DESC";

        LateInit();
    }
}