using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFireLizardEnemy : GameEnemyUnit
{
    public ContentFireLizardEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 15;
        m_maxStamina = 4;
        m_staminaRegen = 3;
        m_power = 8;
        m_attackSFX = AudioHelper.FireBlast;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Fire Lizard";
        
        m_desc = "If this unit kills another, it will <b>transform</b> into a Red Dragon!";

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameDamageReductionKeyword(2), true, false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override int HitUnit(GameUnit other, int damageAmount, bool spendStamina = true, bool isThornsAttack = false, bool canCleave = true)
    {
        int damageTaken = base.HitUnit(other, damageAmount, spendStamina, isThornsAttack, canCleave);

        if (other.m_isDead)
        {
            GameEnemyUnit newRedDragon = new ContentRedDragonEnemy(GameHelper.GetOpponent());
            m_worldUnit.Init(newRedDragon);
            m_worldUnit.SetMoveTarget(other.m_worldUnit.gameObject.transform.position);
            GameHelper.GetOpponent().m_controlledUnits.Remove(this);
            GameHelper.GetOpponent().m_controlledUnits.Add(newRedDragon);

            GetGameTile().SwapUnit(newRedDragon);
        }

        return damageTaken;
    }
}