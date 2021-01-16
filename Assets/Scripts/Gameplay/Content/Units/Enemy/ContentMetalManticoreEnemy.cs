using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMetalManticoreEnemy : GameEnemyUnit
{
    public ContentMetalManticoreEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 65;
        m_maxStamina = 5;
        m_staminaRegen = 5;
        m_attack = 20;
        m_attackSFX = AudioHelper.MetalClangAttack;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Metal Manticore";
        
        m_desc = "Any damage that pierces this unit's damage reduction is tripled.\n";
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameDamageReductionKeyword(20), true, false);
        }
        else
        {
            AddKeyword(new GameDamageReductionKeyword(15), true, false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override int CalculateDamageAmount(int damage, DamageType damageType)
    {
        int damageAmount = base.CalculateDamageAmount(damage, damageType);

        return damageAmount * 3;
    }
}