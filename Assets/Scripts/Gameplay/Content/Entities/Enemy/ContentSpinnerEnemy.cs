using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//For AI:
//Charges at a spot
//Attacks only once per turn, but hits all tiles around it
public class ContentSpinnerEnemy : GameEnemyEntity
{
    public ContentSpinnerEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 7;
        m_maxAP = 4;
        m_apRegen = 2;
        m_power = 3;
        m_apToAttack = 0;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_minWave = 2;

        m_name = "Spinner";
        m_desc = "This guys spins and wins!  Hits all enemies around him";

        m_AIGameEnemyEntity.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AISpinnerChooseTileToMoveStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIMoveToTileStandardStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AISpinnerAttackStep(m_AIGameEnemyEntity));

        LateInit();
    }
}