using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCrumblingAncientEnemy : GameEnemyUnit
{
    private int m_staminaLossAmount = 1;

    public ContentCrumblingAncientEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 100;
        m_maxStamina = 4;
        m_staminaRegen = 3;
        m_power = 20;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Uncommon;

        m_name = "Crumbling Ancient";
        m_desc = "";

        AddKeyword(new GameBrittleKeyword(), true, false);
        if (!GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameEnrageKeyword(new GameLoseStaminaAction(this, m_staminaLossAmount)), true, false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}