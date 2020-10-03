using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentOrcShamanEnemy : GameEnemyUnit
{
    public ContentOrcShamanEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 25;
        m_maxStamina = 6;
        m_staminaRegen = 3;
        m_power = 4;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Rare;

        m_minWave = 3;
        m_maxWave = 4;

        m_name = "Orc Shaman";
        m_desc = "";

        m_keywordHolder.m_keywords.Add(new GameRangeKeyword(2));

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyUnit));
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit));
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit));
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit));

        LateInit();
    }
}