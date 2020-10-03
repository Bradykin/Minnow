using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Prioritize units over buildings
//Priotize targets with Stamina to drain
public class ContentToadEnemy : GameEnemyEntity
{
    public ContentToadEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 7;
        m_maxStamina = 6;
        m_staminaRegen = 3;
        m_power = 2;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Toad";
        m_desc = "On hit, drains Stamina to 1!";

        m_keywordHolder.m_keywords.Add(new GameDamageShieldKeyword(1));

        m_minWave = 2;
        m_maxWave = 2;

        m_AIGameEnemyEntity.AddAIStep(new AIToadSnakeScanTargetsInRangeStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyEntity));

        LateInit();
    }

    public override int HitUnit(GameUnit other, bool spendStamina = true)
    {
        int damageTaken = base.HitUnit(other, spendStamina);

        other.SpendStamina(other.GetCurStamina() - 1);

        return damageTaken;
    }
}