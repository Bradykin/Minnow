using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Prioritize units over buildings
//Priotize targets with Stamina to drain
public class ContentToadEnemy : GameEnemyUnit
{
    public ContentToadEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -1f, 0);

        m_maxHealth = 7;
        m_maxStamina = 6;
        m_staminaRegen = 3;
        m_power = 2;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Toad";
        m_desc = "On hit, drains Stamina to 1!\n";

        AddKeyword(new GameDamageShieldKeyword(1), false);

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameMomentumKeyword(new GameFullHealAction(this)), false);
        }

        m_minWave = 2;
        m_maxWave = 2;

        m_AIGameEnemyUnit.AddAIStep(new AIToadSnakeScanTargetsInRangeStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override int HitUnit(GameUnit other, bool spendStamina = true)
    {
        int damageTaken = base.HitUnit(other, spendStamina);

        if (damageTaken > 0)
        {
            other.SpendStamina(other.GetCurStamina() - 1);
        }

        return damageTaken;
    }
}