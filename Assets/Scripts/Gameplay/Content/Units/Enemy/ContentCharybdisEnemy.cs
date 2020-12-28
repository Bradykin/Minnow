using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Todo ashulman: Make the AI work
public class ContentCharybdisEnemy : GameEnemyUnit
{
    public ContentCharybdisEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 80;
        m_maxStamina = 6;
        m_staminaRegen = 6;
        m_power = 28;
        m_attackSFX = AudioHelper.SlamHeavy;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Charybdis";
        m_desc = "Can use its full turn to smash all adjacent ice.\n";

        AddKeyword(new GameWaterboundKeyword(), true, false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameDamageReductionKeyword(2), true, false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AICharybdisIcebreakStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}