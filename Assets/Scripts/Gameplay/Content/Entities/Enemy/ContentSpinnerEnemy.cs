using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: ashulman
//For AI:
//Charges at a spot
//Attacks only once per turn, but hits all tiles around it
public class ContentSpinnerEnemy : GameEnemyEntity
{
    private bool m_hasAttacked;

    public ContentSpinnerEnemy() : base()
    {
        m_maxHealth = 7;
        m_maxAP = 4;
        m_apRegen = 2;
        m_power = 3;
        m_apToAttack = 0;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Spinner";
        m_desc = "This guys spins and wins!  Hits all enemies around him";

        LateInit();
    }
}