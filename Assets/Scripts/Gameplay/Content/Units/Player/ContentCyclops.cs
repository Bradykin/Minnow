﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCyclops : GameUnit
{
    public ContentCyclops() : base()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.25f, 0);

        m_sightRange = 1;

        m_team = Team.Player;
        m_rarity = GameRarity.Rare;

        m_name = "Cyclops";
        m_desc = "Has a sight range of 1.\n";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconUnit(m_name);
        m_attackSFX = AudioHelper.SlamHeavy;

        LateInit();
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 100;
        m_maxStamina = 3;
        m_staminaRegen = 1;
        m_attack = 70;
    }
}