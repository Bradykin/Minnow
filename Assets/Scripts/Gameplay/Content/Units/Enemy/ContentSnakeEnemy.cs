using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Prioritize units over buildings
//Deprioritze targets with attack <= 0
//Prioritize with high Stamina regen per attack ratio
//TODO Alex - make snake have a chaos ability
public class ContentSnakeEnemy : GameEnemyUnit
{
    public ContentSnakeEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 2;
        m_maxStamina = 5;
        m_staminaRegen = 3;
        m_power = 4;
        m_attackSFX = AudioHelper.SlurpAttack;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Snake";
        m_desc = "When this unit deals damage, it permanently gives -2/-0 to the target.\n";

        AddKeyword(new GameDamageShieldKeyword(), true, false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            m_desc = "When this unit deals damage, it permanently gives -2/-0 to the target and drains 2 stamina.\n";
        }

        m_AIGameEnemyUnit.AddAIStep(new AIToadSnakeScanTargetsInRangeStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override int HitUnit(GameUnit other, int damageAmount, bool spendStamina = true, bool shouldThorns = true, bool canCleave = true)
    {
        int damageTaken = base.HitUnit(other, damageAmount, spendStamina);

        if (damageTaken > 0)
        {
            other.RemoveStats(2, 0, true);
        }

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            other.DrainStamina(2);
        }

        return damageTaken;
    }
}