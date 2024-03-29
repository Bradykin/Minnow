﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRanger : GameUnit
{
    private int m_staminaRegenBoost = 1;
    private int m_rangeBoost = 1;

    public ContentRanger() : base()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.5f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        AddKeyword(new GameRangeKeyword(2), true, false);
        AddKeyword(new GameForestwalkKeyword(), true, false);

        m_name = "Ranger";
        m_desc = $"When in a forest, <b>double</b> attack, +{m_staminaRegenBoost} stamina regen, and +{m_rangeBoost} <b>Range</b>.\n";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }

    public override AudioClip GetAttackSFX()
    {
        if (m_gameTile != null)
        {
            if (m_gameTile.GetTerrain().IsForest())
            {
                return AudioHelper.BowHeavy;
            }
        }

        return AudioHelper.BowLight;
    }

    public override int GetAttack()
    {
        int returnAttack = base.GetAttack();

        if (m_gameTile == null)
        {
            return returnAttack;
        }

        if (m_gameTile.GetTerrain().IsForest())
        {
            returnAttack *= 2;
        }

        return returnAttack;
    }

    public override int GetStaminaRegen()
    {
        int returnStaminaRegen = base.GetStaminaRegen();

        if (m_gameTile == null)
        {
            return returnStaminaRegen;
        }

        if (m_gameTile.GetTerrain().IsForest())
        {
            returnStaminaRegen += m_staminaRegenBoost;
        }

        return returnStaminaRegen;
    }

    public override GameRangeKeyword GetRangeKeyword()
    {
        GameRangeKeyword toReturn = new GameRangeKeyword(0);

        if (base.GetRangeKeyword() != null)
        {
            toReturn.AddKeyword(base.GetRangeKeyword());
        }

        if (GameHelper.IsUnitInWorld(this))
        {
            if (m_gameTile.GetTerrain().IsForest())
            {
                toReturn.AddKeyword(new GameRangeKeyword(m_rangeBoost));
            }
        }

        if (toReturn.m_range == 0)
        {
            toReturn = null;
        }

        return toReturn;
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 15;
        m_maxStamina = 5;
        m_staminaRegen = 2;
        m_attack = 9;
    }
}