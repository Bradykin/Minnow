using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: ashulman
//For AI:
//Ignores units
//Uses full AP to move towards nearest building
//Once at building; stands still until it has full AP, then attacks with all of it
public class ContentSiegebreakerEntity : GameEnemyEntity
{
    public ContentSiegebreakerEntity(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 25;
        m_maxAP = 6;
        m_apRegen = 2;
        m_power = 30;
        m_apToAttack = 2;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Rare;

        m_minWave = 4;

        m_name = "Siegebreaker";
        m_desc = "Do <b>not</b> let this thing get to the buildings!";

        m_AIGameEnemyEntity.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AISiegebreakerChooseTargetToAttackStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIMoveToAttackStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AISiegebreakerAttackUntilOutOfAPStep(m_AIGameEnemyEntity));

        LateInit();

        m_curAP = 0;
    }
}