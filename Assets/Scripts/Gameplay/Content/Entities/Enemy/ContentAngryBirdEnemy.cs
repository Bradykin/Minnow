using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentAngryBirdEnemy : GameEnemyEntity
{
    public ContentAngryBirdEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 4;
        m_maxAP = 8;
        m_apRegen = 5;
        m_power = 2;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Angry Bird";
        m_desc = "Bawkk!";

        m_keywordHolder.m_keywords.Add(new GameFlyingKeyword());

        LateInit();
    }
}