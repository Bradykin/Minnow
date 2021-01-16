using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSpikewolfEnemy : GameEnemyUnit
{
    public ContentSpikewolfEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 20;
        m_maxStamina = 4;
        m_staminaRegen = 4;
        m_attack = 6;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Spikewolf";
        
        m_desc = "";

        AddKeyword(new GameThornsKeyword(8), true, false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameDeathKeyword(new GameGainKeywordRangeAction(this, 2, new GameBleedKeyword())), true, false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}