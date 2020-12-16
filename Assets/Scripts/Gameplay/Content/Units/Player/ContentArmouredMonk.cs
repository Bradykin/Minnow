﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentArmouredMonk : GameUnit
{
    private int m_basePower = 3;

    public int m_qiVal;

    public ContentArmouredMonk()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.5f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        AddKeyword(new GameMomentumKeyword(new GameGainStatsAction(this, 4, 0)), true, false);
        AddKeyword(new GameVictoriousKeyword(new GameMonkHealAction(this, 3)), true, false);

        m_name = "Armoured Monk";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 40;
        m_maxStamina = 5;
        m_staminaRegen = 4;
        m_power = m_basePower;
    }

    public void ResetPower()
    {
        m_power = m_basePower;
        UIHelper.CreateWorldElementNotification($"The power leaves {GetName()}'s body.", false, GetWorldTile().gameObject);
    }
}