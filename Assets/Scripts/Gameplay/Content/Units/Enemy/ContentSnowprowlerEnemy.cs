using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSnowprowlerEnemy : GameEnemyUnit
{
    private int m_bleedAmount = 3;
    private int m_staminaDrainAmount = 1;

    public ContentSnowprowlerEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 14;
        m_maxStamina = 5;
        m_staminaRegen = 4;
        m_power = 1;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Snowprowler";
        m_desc = "";

        AddKeyword(new GameMomentumKeyword(new GameApplyKeywordToOtherOnMomentumAction(this, new GameBleedKeyword(m_bleedAmount))), false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            m_desc += $"When this hits a unit, it drains {m_staminaDrainAmount} Stamina from it.\n";
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override int HitUnit(GameUnit other, int damageAmount, bool spendStamina = true, bool shouldThorns = true, bool canCleave = true)
    {
        bool hasStaminaToDrain = other.GetCurStamina() >= m_staminaDrainAmount;

        int damageTaken = base.HitUnit(other, damageAmount, spendStamina);

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            if (damageTaken > 0)
            {
                if (hasStaminaToDrain)
                {
                    GainStamina(m_staminaDrainAmount);
                    if (!other.m_isDead)
                    {
                        other.SpendStamina(m_staminaDrainAmount);
                    }
                }
            }
        }


        return damageTaken;
    }
}