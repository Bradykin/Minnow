using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFlameImpEnemy : GameEnemyUnit
{
    public ContentFlameImpEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 6;
        m_maxStamina = 7;
        m_staminaRegen = 5;
        m_power = 2;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_minWave = 2;
        m_maxWave = 6;

        m_name = "Flame Imp";
        m_desc = "";

        AddKeyword(new GameMomentumKeyword(new GameFullHealRangeAction(this, 3)), false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameMomentumKeyword(new GameGainStaminaRangeAction(this, 1, 3)), false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackOnceStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}