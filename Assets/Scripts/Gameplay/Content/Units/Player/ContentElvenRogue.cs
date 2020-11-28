﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentElvenRogue : GameUnit
{
    public ContentElvenRogue()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.5f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        AddKeyword(new GameRangeKeyword(2), true, false);
        AddKeyword(new GameMomentumKeyword(new GameGainStatsAction(this, 1, 0)), true, false);

        m_name = "Elven Rogue";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 5;
        m_maxStamina = 7;
        m_staminaRegen = 7;
        m_power = 1;
    }
}