﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPurgeCard : GameCardSpellBase
{
    public ContentPurgeCard()
    {
        m_spellEffect = 5;
        
        m_name = "Purge";
        m_targetType = Target.Unit;
        m_cost = 2;
        m_rarity = GameRarity.Uncommon;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.BuffSpell);
        m_tags.AddTag(GameTag.TagType.Spellpower);
    }

    public override string GetDesc()
    {
        return "Target unit loses all keywords. If it was an allied unit that previously had keywords, it gains +" + m_spellEffect + "/+" + m_spellEffect + " (+" + GetSpellValue() + "/+" + GetSpellValue() + ").\n" + GetModifiedBySpellPowerString();
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        if (targetEntity.GetTeam() == Team.Player)
        {
            if (targetEntity.GetKeywords().Count > 0)
            {
                targetEntity.AddPower(GetSpellValue());
                targetEntity.AddMaxHealth(GetSpellValue());
            }
        }

        targetEntity.GetKeywordHolderForRead().RemoveAllKeywords();
    }
}