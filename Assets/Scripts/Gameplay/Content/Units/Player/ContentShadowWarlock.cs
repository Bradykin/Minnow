﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentShadowWarlock : GameUnit
{
    public ContentShadowWarlock()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.4f, 0);

        m_maxHealth = 12;
        m_maxStamina = 5;
        m_staminaRegen = 2;
        m_power = 2;

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;
        AddKeyword(new GameSpellcraftKeyword(new GameGainStatsAction(this, 1, 0)), false);
        AddKeyword(new GameRangeKeyword(3), false);

        m_name = "Shadow Warlock";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }
}