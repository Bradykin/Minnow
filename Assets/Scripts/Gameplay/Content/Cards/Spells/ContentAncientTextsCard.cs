using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentAncientTextsCard : GameCardSpellBase
{
    public ContentAncientTextsCard()
    {
        m_spellEffect = 4;

        m_name = "Ancient Texts";
        m_targetType = Target.None;
        m_cost = 1;
        m_rarity = GameRarity.Uncommon;

        m_playerUnlockLevel = 2;

        SetupBasicData();

        m_keywordHolder.m_keywords.Add(new GameKnowledgeableKeyword(null));

        m_tags.AddTag(GameTag.TagType.Knowledgeable);
        m_tags.AddTag(GameTag.TagType.Spellcraft);
        m_tags.AddTag(GameTag.TagType.Spellpower);
        m_tags.AddTag(GameTag.TagType.UtilitySpell);
    }

    public override string GetDesc()
    {
        string spString = "";
        if (HasSpellPower())
        {
            spString = GetSpellPowerString();
        }

        return "Trigger <b>Knowledgeable</b> " + m_spellEffect + spString + " times.\n" + GetModifiedBySpellPowerString();
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
        for (int i = 0; i < spellValue; i++)
        {
            player.TriggerKnowledgeable();
        }
    }
}