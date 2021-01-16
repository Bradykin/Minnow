using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFlameImpEnemy : GameEnemyUnit
{
    int m_range = 3;
    int m_attackIncreaseCount = 5;
    
    public ContentFlameImpEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 4;
        m_maxStamina = 7;
        m_staminaRegen = 5;
        m_attack = 2;
        m_attackSFX = AudioHelper.FireBlast;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Flame Imp";
        m_desc = "";

        AddKeyword(new GameLavawalkKeyword(), true, false);
        AddKeyword(new GameMomentumKeyword(new GameGainKeywordRangeAction(this, m_range, new GameDamageShieldKeyword())), true, false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameMomentumKeyword(new GameGainStatsRangeAction(this, m_attackIncreaseCount, 0, m_range)), true, false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackOnceStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}