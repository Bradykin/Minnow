﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertSwordsman : GameUnit
{
    public ContentDesertSwordsman()
    {
        m_worldTilePositionAdjustment = new Vector3(0.1f, 0.3f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        AddKeyword(new GameCleaveKeyword(), true, false);

        m_name = "Desert Swordsman";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 10;
        m_maxStamina = 5;
        m_staminaRegen = 4;
        m_power = 30;
    }
}