﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMechanizedBeast : GameUnit
{
    public ContentMechanizedBeast()
    {
        m_team = Team.Player;
        m_rarity = GameRarity.Starter;

        m_name = "Mechanized Beast";
        m_desc = "Starts at full Stamina.";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconUnit(m_name);

        SetUnitLevel(GetUnitLevel());

        LateInit();
    }

    public override void OnSummon()
    {
        base.OnSummon();

        m_curStamina = m_maxStamina;
    }

    public override void SetUnitLevel(int level)
    {
        base.SetUnitLevel(level);

        m_maxHealth = 5;
        m_maxStamina = 6;
        m_staminaRegen = 2;
        m_power = 5;

        if (m_unitLevel >= 1)
        {
            m_maxStamina = 10;
        }

        if (m_unitLevel >= 2)
        {
            m_keywordHolder.m_keywords.Add(new GameMountainwalkKeyword());
        }
    }
}