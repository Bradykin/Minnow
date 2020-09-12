using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWerewolfEnemy : GameEnemyEntity
{
    public ContentWerewolfEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 12;
        m_maxAP = 4;
        m_apRegen = 2;
        m_power = 5;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Uncommon;

        m_name = "Werewolf";
        m_desc = "This thing never stops healing!";

        LateInit();

        m_keywordHolder.m_keywords.Add(new GameRegenerateKeyword(m_maxHealth));
    }
}