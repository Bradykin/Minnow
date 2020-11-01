using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Prioritize units over buildings
//Deprioritze targets with attack <= 0
//Prioritize with high Stamina regen per attack ratio
public class ContentSnakeEnemy : GameEnemyUnit
{
    public ContentSnakeEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 2;
        m_maxStamina = 6;
        m_staminaRegen = 3;
        m_power = 2;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Snake";
        m_desc = "On hit, permanently give -2/-0.\n";

        m_minWave = 4;
        m_maxWave = 4;

        int damageShield = 2;
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            damageShield = 4;
        }
        AddKeyword(new GameDamageShieldKeyword(damageShield), false);

        m_AIGameEnemyUnit.AddAIStep(new AIToadSnakeScanTargetsInRangeStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override int HitUnit(GameUnit other, int damageAmount, bool spendStamina = true, bool shouldThorns = true)
    {
        int damageTaken = base.HitUnit(other, damageAmount, spendStamina);

        if (damageTaken > 0)
        {
            other.RemoveStats(2, 0);
        }

        return damageTaken;
    }
}