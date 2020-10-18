﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentInjuredTroll : GameUnit
{
    public ContentInjuredTroll()
    {
        m_maxHealth = 45;
        m_maxStamina = 8;
        m_staminaRegen = 4;
        m_power = 12;

        AddKeyword(new GameRegenerateKeyword(20), false);

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;

        m_name = "Injured Troll";
        m_desc = "Starts at 1 health and 0 Stamina.";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }

    public override void OnSummon()
    {
        base.OnSummon();

        m_curHealth = 1;
        m_curStamina = 0;
    }
}
