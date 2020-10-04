﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCardSpellBase : GameCard
{
    //This is for keywords on the card that we want to display info for.
    protected GameKeywordHolder m_keywordHolder = new GameKeywordHolder();

    protected int m_spellEffect;

    protected void SetupBasicData()
    {
        if (m_shouldExile)
        {
            m_typeline += "Exile ";
        }
        m_typeline += "Spell - " + m_targetType.ToString();
        m_icon = UIHelper.GetIconCard(m_name);

        AddBasicTags();
    }

    private void AddBasicTags()
    {
        if (m_shouldExile)
        {
            m_tags.AddTag(GameTag.TagType.Exile);
        }

        if (m_cost >= 2)
        {
            m_tags.AddTag(GameTag.TagType.HighCost);
        }

        if (m_cost == 0)
        {
            m_tags.AddTag(GameTag.TagType.LowCost);
        }
    }

    protected virtual int GetSpellValue()
    {
        int toReturn = m_spellEffect + GameHelper.GetPlayer().GetSpellPower();

        if (toReturn < 0)
        {
            toReturn = 0;
        }

        return toReturn;
    }

    protected string GetDamageDescString()
    {
        string spString = "";
        if (HasSpellPower())
        {
            spString = GetSpellPowerString();
        }

        return "Deal " + m_spellEffect + spString + " damage.\n";
    }

    protected string GetHealDescString()
    {
        string spString = "";
        if (HasSpellPower())
        {
            spString = GetSpellPowerString();
        }

        return "Heal for " + m_spellEffect + spString + ".\n";
    }

    protected string GetSpellPowerString()
    {
        if (GetSpellValue() == m_spellEffect)
        {
            return "";
        }
        else
        {
            return " (" + GetSpellValue() + ")";
        }
    }

    protected virtual bool CanTriggerSpellPower()
    {
        return true;
    }

    protected string GetModifiedBySpellPowerString()
    {
        return "<i>(Modified by spell power)</i>";
    }

    protected bool HasSpellPower()
    {
        return GetSpellValue() != m_spellEffect;
    }

    public override void PlayCard()
    {
        base.PlayCard();

        Globals.m_spellsPlayedThisTurn++;
        TriggerSpellcraft(null);
    }

    public override void PlayCard(GameBuildingBase targetBuilding)
    {
        base.PlayCard(targetBuilding);

        Globals.m_spellsPlayedThisTurn++;
        TriggerSpellcraft(targetBuilding.GetGameTile());
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        base.PlayCard(targetUnit);

        Globals.m_spellsPlayedThisTurn++;
        if (CanTriggerSpellPower())
        {
            TriggerSpellcraft(targetUnit.GetGameTile());
        }
    }

    public override void PlayCard(GameTile targetTile)
    {
        base.PlayCard(targetTile);

        Globals.m_spellsPlayedThisTurn++;
        TriggerSpellcraft(targetTile);
    }

    protected void TriggerSpellcraft(GameTile tileCastAt)
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        player.TriggerSpellcraft(m_targetType, tileCastAt);
    }

    public GameKeywordHolder GetKeywordHolderForRead()
    {
        return m_keywordHolder;
    }

    //TODO: nmartino - Go through the overrides and find a way to make them function w/ this.
    public override string GetDesc()
    {
        string desc = m_desc;

        /*if (GetKeywordHolderForRead().m_keywords.Count > 0 && (desc != ""))
        {
            desc += "\n";
        }

        desc += GetKeywordHolderForRead().GetDesc();
        */
        return desc;
    }
}