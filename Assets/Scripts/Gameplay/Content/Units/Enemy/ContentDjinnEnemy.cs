using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDjinnEnemy : GameEnemyUnit
{
    public ContentDjinnEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0, 0);

        m_maxHealth = 34;
        m_maxStamina = 6;
        m_staminaRegen = 5;
        m_power = 14;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Djinn";
        m_desc = "";

        AddKeyword(new GameFlyingKeyword(), false);
        AddKeyword(new GameDeathKeyword(new GameGainTempMagicPowerAction(1)), false);

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameDeathKeyword(new GameGainStatsRangeAction(this, 5, 5, 3)), false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}