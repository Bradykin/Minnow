using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentShadeEnemy : GameEnemyUnit
{
    public ContentShadeEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 10;
        m_maxStamina = 5;
        m_staminaRegen = 5;
        m_attack = 10;
        m_attackSFX = AudioHelper.DaggerLight;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Uncommon;

        m_name = "Shade";
        m_desc = "";

        AddKeyword(new GameFlyingKeyword(), true, false);
        AddKeyword(new GameDamageShieldKeyword(), true, false);

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameDeathKeyword(new GameExplodeAction(this, 15, 2)), true, false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
}