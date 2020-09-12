using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLichEnemy : GameEnemyEntity
{
    public ContentLichEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 80;
        m_maxAP = 10;
        m_apRegen = 5;
        m_power = 7;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Event;
        m_isBoss = true;

        m_name = "Lich";
        m_desc = "The final boss.  Kill it, and win. (Not yet implemented)";

        m_keywordHolder.m_keywords.Add(new GameRangeKeyword(3));
        m_keywordHolder.m_keywords.Add(new GameSpellcraftKeyword(new GameGainPowerAction(this, 3)));
        m_keywordHolder.m_keywords.Add(new GameSpellcraftKeyword(new GameHealAction(this, Mathf.FloorToInt((float)m_maxHealth / 2f))));
        m_keywordHolder.m_keywords.Add(new GameFlyingKeyword());

        LateInit();
    }

    public override void Die()
    {
        WorldController.Instance.WinGame();

        base.Die();
    }
}