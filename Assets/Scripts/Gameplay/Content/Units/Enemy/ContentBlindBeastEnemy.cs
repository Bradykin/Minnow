using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBlindBeastEnemy : GameEnemyUnit
{
    public ContentBlindBeastEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 8;
        m_maxStamina = 5;
        m_staminaRegen = 3;
        m_power = 6;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Blind Beast";
        m_desc = $"Can only detect targets within range 1 of itself.\n";

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameDamageReductionKeyword(2), true, false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIBlindBeastScanTargetsInRangeStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIBlindBeastScanTargetsInRangeStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}