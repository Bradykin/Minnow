using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentInsightCard : GameCardSpellBase
{
    public ContentInsightCard()
    {
        m_spellEffect = 3;

        m_name = "Insight";
        m_playDesc += "Spellcraft is done here...";
        m_targetType = Target.None;
        m_cost = 1;
        m_rarity = GameRarity.Rare;

        SetupBasicData();
    }

    public override string GetDesc()
    {
        string spString = "";
        if (HasSpellPower())
        {
            spString = GetSpellPowerString();
        }

        return "Trigger spellcraft " + m_spellEffect + spString + " times (including the trigger from this card).\n" + GetModifiedBySpellPowerString();
    }

    public override void PlayCard()
    {
        if (!IsValidToPlay())
        {
            return;
        }

        base.PlayCard();

        GamePlayer player = GameHelper.GetPlayer();
        if (player == null)
        {
            return;
        }

        for (int i = 0; i < GetSpellValue() -1; i++)
        {
            TriggerSpellcraft();
        }
    }
}