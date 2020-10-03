using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSiegebreakerEntity : GameEnemyEntity
{
    public ContentSiegebreakerEntity(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 90;
        m_maxStamina = 6;
        m_staminaRegen = 2;
        m_power = 100;
        m_staminaToAttack = 2;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Rare;
        m_shouldAlwaysPassEnemies = true;

        m_minWave = 4;
        m_maxWave = 6;

        m_name = "Siegebreaker";
        m_desc = "Can move through your units.  Will only attack at full Stamina, and only buildings.";

        m_AIGameEnemyEntity.AddAIStep(new AISiegebreakerScanTargetsInRangeStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AISiegebreakerChooseTargetToAttackStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AISiegebreakerMoveToTargetStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AISiegebreakerAttackUntilOutOfStaminaStep(m_AIGameEnemyEntity));

        LateInit();
    }
}