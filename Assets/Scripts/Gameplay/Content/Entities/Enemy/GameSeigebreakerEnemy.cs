﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: ashulman
//For AI:
//Ignores units
//Uses full AP to move towards nearest building
//Once at building; stands still until it can attack, then attacks
public class GameSeigebreakerEntity : GameEntity
{
    public GameSeigebreakerEntity()
    {
        m_maxHealth = 30;
        m_maxAP = 6;
        m_apRegen = 2;
        m_power = 30;
        m_apToAttack = m_maxAP;

        m_team = Team.Enemy;

        m_name = "Seigebreaker";
        m_desc = "Do <b>not</b> let this thing get to the buildings!";
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();

        m_curAP = 0;
    }
}