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
        return " (" + GetSpellValue() + ")";
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

        TriggerSpellcraft();
    }

    public override void PlayCard(GameBuildingBase targetBuilding)
    {
        base.PlayCard(targetBuilding);

        TriggerSpellcraft();
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        base.PlayCard(targetEntity);

        TriggerSpellcraft();
        Globals.m_spellsPlayedThisTurn++;
    }

    public override void PlayCard(GameTile targetTile)
    {
        base.PlayCard(targetTile);

        TriggerSpellcraft();
    }

    protected void TriggerSpellcraft()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        player.TriggerSpellcraft();
    }
}
