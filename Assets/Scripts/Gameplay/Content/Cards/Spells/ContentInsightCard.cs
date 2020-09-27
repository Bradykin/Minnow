using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentInsightCard : GameCardSpellBase
{
    public ContentInsightCard()
    {
        m_spellEffect = 4;

        m_name = "Insight";
        m_targetType = Target.None;
        m_cost = 1;
        m_rarity = GameRarity.Rare;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.UtilitySpell);
        m_tags.AddTag(GameTag.TagType.Spellcraft);
        m_tags.AddTag(GameTag.TagType.Spellpower);
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

        int spellValue = GetSpellValue();
        for (int i = 0; i < spellValue - 1; i++)
        {
            TriggerSpellcraft(null);
        }
    }
}