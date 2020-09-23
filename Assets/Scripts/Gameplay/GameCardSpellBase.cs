using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCardSpellBase : GameCard
{
    protected int m_spellEffect;

    protected void SetupBasicData()
    {
        if (m_shouldExile)
        {
            m_typeline += "Exile ";
        }
        m_typeline += "Spell - " + m_targetType.ToString();
        m_icon = UIHelper.GetIconCard(m_name);
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
        return "(This is modified by spell power)";
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

    public override void PlayCard(GameEntity targetEntity)
    {
        base.PlayCard(targetEntity);

        Globals.m_spellsPlayedThisTurn++;
        if (CanTriggerSpellPower())
        {
            TriggerSpellcraft(targetEntity.GetGameTile());
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
}
