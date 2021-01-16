using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBasiliskEnemy : GameEnemyUnit
{
    public ContentBasiliskEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 16;
        m_maxStamina = 4;
        m_staminaRegen = 4;
        m_attack = 6;
        m_attackSFX = AudioHelper.Roar;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;


        m_name = "Basilisk";
        m_desc = $"When this unit hits another, it gives them <b>Rooted</b> until end of wave. If they were already rooted, they instead get <b>Brittle</b> until end of wave.\n";

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            m_desc += "If they were already <b>Brittle</b>, then it becomes <b>permanent</b>.\n";
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
            if (other.GetRootedKeyword() == null)
            {
                other.AddKeyword(new GameRootedKeyword(), false, false);
            }
            else
            {
                if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
                {
                    if (other.GetBrittleKeyword() == null)
                    {
                        other.AddKeyword(new GameBrittleKeyword(), false, false);
                    }
                    else if (!other.GetBrittleKeyword().m_isPermanent)
                    {
                        other.GetBrittleKeyword().m_isPermanent = true;
                    }
                }
                else
                {
                    other.AddKeyword(new GameBrittleKeyword(), false, false);
                }
            }
        }

        return amount;
    }
}