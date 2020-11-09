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

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Charybdis";
        m_desc = "";

        AddKeyword(new GameWaterboundKeyword(), false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameDamageReductionKeyword(2), false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AICharybdisIcebreakStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}