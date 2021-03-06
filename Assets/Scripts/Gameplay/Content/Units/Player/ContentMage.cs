﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMage : GameUnit
{
    public ContentMage() : base()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.3f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        AddKeyword(new GameRangeKeyword(2), true, false);
        AddKeyword(new GameKnowledgeableKeyword(new GameGainRandomSpellAction(1)), true, false);

        m_name = "Mage";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);
        m_attackSFX = AudioHelper.SpellAttackMedium;

        LateInit();
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 2;
        m_maxStamina = 5;
        m_staminaRegen = 2;
        m_attack = 8;
    }
}