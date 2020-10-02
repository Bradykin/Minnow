using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//For AI:
//Ignores units
//Uses full AP to move towards nearest building
//Once at building; stands still until it has full AP, then attacks with all of it
public class ContentSiegebreakerEntity : GameEnemyEntity
{
    public ContentSiegebreakerEntity(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 90;
        m_maxAP = 6;
        m_apRegen = 2;
        m_power = 100;
        m_apToAttack = 2;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Rare;
        m_shouldAlwaysPassEnemies = true;

        m_minWave = 4;
        m_maxWave = 6;

        m_name = "Siegebreaker";
        m_desc = "Can move through your units.  Will only attack at full AP, and only buildings. Can hit buildings that have a unit on top of them.";

        m_AIGameEnemyEntity.AddAIStep(new AISiegebreakerScanTargetsInRangeStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AISiegebreakerChooseTargetToAttackStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AISiegebreakerMoveToTargetStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AISiegebreakerAttackUntilOutOfAPStep(m_AIGameEnemyEntity));

        LateInit();
    }
}