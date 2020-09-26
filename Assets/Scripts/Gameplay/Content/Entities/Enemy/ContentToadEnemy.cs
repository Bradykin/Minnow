using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Prioritize units over buildings
//Priotize targets with AP to drain
public class ContentToadEnemy : GameEnemyEntity
{
    public ContentToadEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 7;
        m_maxAP = 6;
        m_apRegen = 3;
        m_power = 2;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Toad";
        m_desc = "Don't let this thing hit you; it'll drain your AP to 1!";

        m_keywordHolder.m_keywords.Add(new GameDamageShieldKeyword(1));

        m_minWave = 2;
        m_maxWave = 2;

        m_AIGameEnemyEntity.AddAIStep(new AIToadSnakeScanTargetsInRangeStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIAttackUntilOutOfAPStandardStep(m_AIGameEnemyEntity));

        LateInit();
    }

    public override int HitEntity(GameEntity other, bool spendAP = true)
    {
        int damageTaken = base.HitEntity(other, spendAP);

        other.SpendAP(other.GetCurAP() - 1);

        return damageTaken;
    }
}