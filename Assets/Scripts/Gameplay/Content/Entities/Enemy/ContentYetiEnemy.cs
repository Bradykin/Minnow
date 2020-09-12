using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentYetiEnemy : GameEnemyEntity
{
    public ContentYetiEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 10;
        m_maxAP = 4;
        m_apRegen = 2;
        m_power = 5;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Yei";
        m_desc = "Is it... is it throwing snowballs?";

        m_keywordHolder.m_keywords.Add(new GameRangeKeyword(3));

        LateInit();
    }
}