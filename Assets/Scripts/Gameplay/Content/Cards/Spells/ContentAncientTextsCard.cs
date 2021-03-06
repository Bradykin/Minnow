﻿using System.Collections;
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

        SetupBasicData();

        m_keywordHolder.AddKeyword(new GameKnowledgeableKeyword(null));

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Knowledgeable, 3);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.UtilitySpell);

        m_onPlaySFX = AudioHelper.MiscEffect;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return $"Trigger <b>Knowledgeable</b> {UIHelper.GetMagicPowerColoredValue(m_spellEffect + mpString)} times.";
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