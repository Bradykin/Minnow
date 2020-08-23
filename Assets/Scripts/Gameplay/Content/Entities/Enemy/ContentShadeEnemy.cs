﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: ashulman
//For AI:
//Goes for units exclusivly.  Doesn't target buildings.
//Hit and run if possible.  Meant to be really annoying to pin down
public class ContentShadeEnemy : GameEntity
{
    public ContentShadeEnemy()
    {
        m_maxHealth = 7;
        m_maxAP = 4;
        m_apRegen = 4;
        m_power = 2;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Uncommon;

        m_name = "Shade";
        m_desc = "Hit and run, with some irritating healing.  Try focusing it with magic!";
        m_keywordHolder.m_keywords.Add(new GameRegenerateKeyword(m_maxHealth));

        LateInit();
    }
}