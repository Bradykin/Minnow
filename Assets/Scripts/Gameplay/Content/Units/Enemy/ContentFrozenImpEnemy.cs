using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFrozenImpEnemy : GameEnemyUnit
{
    public ContentFrozenImpEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 9;
        m_maxStamina = 5;
        m_staminaRegen = 5;
        m_power = 2;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Frozen Imp";
        m_desc = "When this unit hits another, the target gets Rooted until end of turn.\n";

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            m_staminaToAttack = 1;
            m_maxStamina++;
            m_staminaRegen++;
            m_desc += " Only takes 1 Stamina to attack.\n";
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIFrozenImpChooseTargetToAttackStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackOnceRepeatStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override int HitUnit(GameUnit other, int damageAmount, bool spendStamina = true, bool shouldThorns = true, bool canCleave = true)
    {
        int toReturn = base.HitUnit(other, damageAmount, spendStamina, shouldThorns);

        if (!other.m_isDead && other.GetRootedKeyword() == null)
        {
            other.AddKeyword(new GameRootedKeyword(), false);
            GameHelper.GetPlayer().AddScheduledAction(ScheduledActionTime.EndOfTurn, new GameLoseKeywordAction(other, new GameRootedKeyword()));
        }

        return toReturn;
    }
}