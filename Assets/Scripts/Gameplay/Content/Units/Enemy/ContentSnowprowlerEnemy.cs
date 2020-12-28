using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSnowprowlerEnemy : GameEnemyUnit
{
    public ContentSnowprowlerEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 14;
        m_maxStamina = 5;
        m_staminaRegen = 4;
        m_power = 5;
        m_attackSFX = AudioHelper.Roar;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Snowprowler";
        m_desc = "";

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameMomentumKeyword(new GameApplyKeywordToOtherOnMomentumAction(this, new GameBleedKeyword())), false, false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}