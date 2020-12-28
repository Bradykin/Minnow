using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentValgulaEnemy : GameEnemyUnit
{
    public ContentValgulaEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 9;
        m_maxStamina = 4;
        m_staminaRegen = 3;
        m_power = 4;
        m_attackSFX = AudioHelper.RaptorAttack;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Valgula";
        m_desc = "";

        AddKeyword(new GameFlyingKeyword(), true, false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameSpellcraftKeyword(new GameGainStatsAction(this, 2, 2)), true, false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}