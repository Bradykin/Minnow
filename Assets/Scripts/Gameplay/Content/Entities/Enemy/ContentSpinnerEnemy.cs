using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSpinnerEnemy : GameEnemyUnit
{
    public ContentSpinnerEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 7;
        m_maxStamina = 6;
        m_staminaRegen = 4;
        m_power = 4;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_minWave = 2;
        m_maxWave = 4;

        m_name = "Spinner";
        m_desc = "";

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameRangeKeyword(2), false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}