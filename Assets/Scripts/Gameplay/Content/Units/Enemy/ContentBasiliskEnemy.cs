using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBasiliskEnemy : GameEnemyUnit
{
    public ContentBasiliskEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 18;
        m_maxStamina = 4;
        m_staminaRegen = 4;
        m_power = 7;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Basilisk";
        m_desc = $"When this unit hits another, it gives them Brittle until end of wave.\n";

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameRangeKeyword(2), true, false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override int HitUnit(GameUnit other, int damageAmount, bool spendStamina = true, bool shouldThorns = true, bool canCleave = true)
    {
        int amount =  base.HitUnit(other, damageAmount, spendStamina, shouldThorns);

        if (!other.m_isDead)
        {
            other.AddKeyword(new GameBrittleKeyword(), false, true);
        }

        return amount;
    }
}