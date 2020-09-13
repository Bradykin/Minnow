using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLichEnemy : GameEnemyEntity
{
    public ContentLichEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 80;
        m_maxAP = 10;
        m_apRegen = 6;
        m_power = 15;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Event;
        m_isBoss = true;

        m_minWave = Constants.FinalWaveNum;

        m_name = "Lich";
        m_desc = "The final boss.  Kill it, and win.";

        m_keywordHolder.m_keywords.Add(new GameRangeKeyword(3));
        m_keywordHolder.m_keywords.Add(new GameRegenerateKeyword(20));
        m_keywordHolder.m_keywords.Add(new GameFlyingKeyword());

        m_AIGameEnemyEntity.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIChooseTargetToAttackStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIAttackUntilOutOfAPStep(m_AIGameEnemyEntity));

        LateInit();
    }

    public override void Die()
    {
        WorldController.Instance.WinGame();

        base.Die();
    }
}