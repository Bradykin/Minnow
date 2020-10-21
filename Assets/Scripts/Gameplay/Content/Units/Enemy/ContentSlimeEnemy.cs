using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSlimeEnemy : GameEnemyUnit
{
    public ContentSlimeEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 4;
        m_maxStamina = 4;
        m_staminaRegen = 2;
        m_power = 2;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_minWave = 1;
        m_maxWave = 2;

        m_name = "Slime";
        m_desc = "";

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameEnrageKeyword(new GameHealAction(this, 2)), false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}