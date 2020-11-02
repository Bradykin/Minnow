using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBlindBeastEnemy : GameEnemyUnit
{
    int m_effectRange = 4;
    int m_effectIncrease = 1;
    
    public ContentBlindBeastEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 6;
        m_maxStamina = 5;
        m_staminaRegen = 3;
        m_power = 5;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_minWave = 1;
        m_maxWave = 2;

        m_name = "Blind Beast";
        m_desc = $"Can only detect targets within 1 range of itself.";

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameMomentumKeyword(new GameGainStatsAction(this, 1, 1)), false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIBlindBeastScanTargetsInRangeStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}