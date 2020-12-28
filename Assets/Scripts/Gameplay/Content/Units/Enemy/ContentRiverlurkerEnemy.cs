using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRiverlurkerEnemy : GameEnemyUnit
{
    private int m_deathRollDamageMultiplier = 3;
    
    public ContentRiverlurkerEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 30;
        m_maxStamina = 5;
        m_staminaRegen = 5;
        m_power = 8;
        m_attackSFX = AudioHelper.SlamHeavy;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Riverlurker";
        m_desc = "This unit uses double the stamina to move when not in water.\n";

        AddKeyword(new GameMomentumKeyword(new GameApplyKeywordToOtherOnMomentumAction(this, new GameBleedKeyword())), true, false);
        AddKeyword(new GameWaterwalkKeyword(), true, false);

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            m_desc += $"When this unit attacks a bleeding target, it spends all Stamina and deals {m_deathRollDamageMultiplier} times damage.";
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override int GetDamageToDealTo(GameUnit targetToAttack)
    {
        int toReturn = base.GetDamageToDealTo(targetToAttack);
        
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility) && targetToAttack != null && targetToAttack is GameUnit gameUnit && gameUnit.GetBleedKeyword() != null)
        {
            toReturn *= m_deathRollDamageMultiplier;
        }

        return toReturn;
    }

    public override int HitUnit(GameUnit other, int damageAmount, bool spendStamina = true, bool shouldThorns = true, bool canCleave = true)
    {
        int damage = base.GetDamageToDealTo(other);

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility) && other != null && other is GameUnit gameUnit && gameUnit.GetBleedKeyword() != null)
        {
            damage *= m_deathRollDamageMultiplier;
            SpendStamina(GetStaminaToAttack(other));
            UIHelper.CreateWorldElementNotification("Riverlurker does a death roll!", false, GetWorldTile().gameObject);
        }

        return base.HitUnit(other, damage, spendStamina, shouldThorns, canCleave);
    }

    public override int GetStaminaToAttack(GameElementBase targetToAttack)
    {
        if (targetToAttack == null)
        {
            return base.GetStaminaToAttack(targetToAttack);
        }

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility) && targetToAttack != null && targetToAttack is GameUnit gameUnit && gameUnit.GetBleedKeyword() != null)
        {
            return Mathf.Max(2, GetCurStamina());
        }
        else
        {
            return base.GetStaminaToAttack(targetToAttack);
        }
    }
}