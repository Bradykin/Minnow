using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentShadeEnemy : GameEnemyUnit
{
    public ContentShadeEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 12;
        m_maxStamina = 6;
        m_staminaRegen = 4;
        m_power = 7;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Uncommon;

        m_name = "Shade";
        m_desc = "";

        AddKeyword(new GameFlyingKeyword(), false);
        AddKeyword(new GameDamageShieldKeyword(2), false);

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameDeathKeyword(new GameExplodeAction(this, 15, 2)), false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}