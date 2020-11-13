﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDwarvenSoldier : GameUnit
{
    public ContentDwarvenSoldier()
    {
        m_worldTilePositionAdjustment = new Vector3(-0.15f, 0, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Starter;

        m_name = "Dwarven Soldier";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);

        m_maxHealth = 20;
        m_maxStamina = 5;
        m_staminaRegen = 3;
        m_power = 6;

        LateInit();
    }
}
