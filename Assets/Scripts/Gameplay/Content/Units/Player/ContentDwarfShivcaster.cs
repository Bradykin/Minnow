﻿using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDwarfShivcaster : GameUnit
{
    public ContentDwarfShivcaster()
    {
        m_maxHealth = 16;
        m_maxStamina = 5;
        m_staminaRegen = 2;
        m_power = 2;

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;

        m_name = "Dwarf Shivcaster";
        m_desc = "Shivs no longer trigger <b>Spellcraft</b>.\n";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);

        AddKeyword(new GameSpellcraftKeyword(new GameShivNearbyAction(this, 2, 2)), false);
        AddKeyword(new GameRangeKeyword(2), false);
        AddKeyword(new GameShivKeyword(), false);

        LateInit();
    }
}