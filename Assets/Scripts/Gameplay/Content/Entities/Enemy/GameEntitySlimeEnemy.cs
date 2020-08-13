﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntitySlimeEnemy : GameEntityBase
{
    public GameEntitySlimeEnemy()
    {
        m_maxHealth = 4;
        m_maxAP = 3;
        m_apRegen = 3;
        m_power = 1;
        m_toughness = 0;

        m_team = Team.Enemy;

        m_name = "Slime";
        m_desc = "What a basic slime.  LOL!";
        m_icon = null;

        LateInit();
    }
}