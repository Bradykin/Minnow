using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSiegebreakerUnit : GameEnemyUnit
{
    public ContentSiegebreakerUnit(GameOpponent gameOpponent) : base(gameOpponent)
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
        m_desc = "Can move through your units.  Will only attack at full Stamina, and only buildings.  Can hit buildings that have a unit on top of them.";

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameFlyingKeyword(), false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AISiegebreakerScanTargetsInRangeStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AISiegebreakerChooseTargetToAttackStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AISiegebreakerMoveToTargetStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AISiegebreakerAttackUntilOutOfStaminaStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}