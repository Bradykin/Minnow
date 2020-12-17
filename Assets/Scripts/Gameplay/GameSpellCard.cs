﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCardSpellBase : GameCard
{
    protected AudioHelper.SpellAudioCategory m_audioCategory;

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
        int toReturn = m_spellEffect;

        GamePlayer player = GameHelper.GetPlayer();

        if (player != null)
        {
            toReturn += player.GetMagicPower();
        }

        if (toReturn < 0)
        {
            toReturn = 0;
        }

        return toReturn;
    }

    protected string GetDamageDescString(bool useMagicPower = true)
    {
        string mpString = "";
        if (useMagicPower && HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return "Deal " + m_spellEffect + mpString + " damage.\n";
    }

    protected string GetHealDescString()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return "Heal for " + m_spellEffect + mpString + ".\n";
    }

    protected string GetMagicPowerString()
    {
        if (GetSpellValue() == m_spellEffect)
        {
            return "";
        }
        else
        {
            return "(" + GetSpellValue() + ")";
        }
    }

    protected virtual bool CanTriggerSpellcraft()
    {
        return true;
    }

    protected string GetModifiedByMagicPowerString()
    {
        return "<i>(Modified by <b>Magic Power</b>)</i>";
    }

    protected bool HasMagicPower()
    {
        return GetSpellValue() != m_spellEffect;
    }

    private void PlayCardImpl()
    {
        GameHelper.GetPlayer().m_spellsPlayedThisTurn++;
    }

    public override void PlayCard()
    {
        base.PlayCard();

        PlayCardImpl();

        TriggerSpellcraft(null);

        HandleAudio();
    }

    public override void PlayCard(GameBuildingBase targetBuilding)
    {
        base.PlayCard(targetBuilding);

        PlayCardImpl();

        TriggerSpellcraft(targetBuilding.GetGameTile());

        HandleAudio();
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        base.PlayCard(targetUnit);

        PlayCardImpl();

        if (CanTriggerSpellcraft())
        {
            TriggerSpellcraft(targetUnit.GetGameTile());
        }

        HandleAudio();
    }

    public override void PlayCard(GameTile targetTile)
    {
        base.PlayCard(targetTile);

        PlayCardImpl();

        TriggerSpellcraft(targetTile);

        HandleAudio();
    }

    protected virtual void HandleAudio()
    {
        AudioHelper.PlaySFX(m_audioCategory);
    }

    protected void TriggerSpellcraft(GameTile tileCastAt)
    {
        GamePlayer player = GameHelper.GetPlayer();
        GameOpponent opponent = GameHelper.GetOpponent();

        if (player == null)
        {
            return;
        }

        player.TriggerSpellcraft(m_targetType, tileCastAt);
        opponent.TriggerSpellcraft(m_targetType, tileCastAt);

        //If we have Impalium, do it again
        if (GameHelper.HasRelic<ContentImpaliumRelic>())
        {
            player.TriggerSpellcraft(m_targetType, tileCastAt);
        }
    }

    public GameKeywordHolder GetKeywordHolderForRead()
    {
        return m_keywordHolder;
    }

    public override string GetDesc()
    {
        return m_desc;
    }
}
