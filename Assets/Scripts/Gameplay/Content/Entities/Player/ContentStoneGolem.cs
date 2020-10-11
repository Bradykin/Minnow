﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentStoneGolem : GameUnit
{
    public ContentStoneGolem()
    {
        m_team = Team.Player;
        m_rarity = GameRarity.Starter;

        m_name = "Stone Golem";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }

    public override void SetUnitLevel(int level)
    {
        base.SetUnitLevel(level);

        m_maxHealth = 40;
        m_maxStamina = 2;
        m_staminaRegen = 1;
        m_power = 1;

        if (m_unitLevel >= 1)
        {
            m_maxStamina = 4;
            m_staminaRegen = 2;
        }

        if (m_unitLevel >= 2)
        {
            m_power = 10;
        }
    }
}