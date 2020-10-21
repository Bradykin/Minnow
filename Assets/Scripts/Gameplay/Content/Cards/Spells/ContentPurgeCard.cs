using System.Collections;
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

        m_playerUnlockLevel = 4;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.BuffSpell);
        m_tags.AddTag(GameTag.TagType.Spellpower);
    }

    public override string GetDesc()
    {
        if (m_spellEffect != GetSpellValue())
        {
            return "Target non-elite unit loses all keywords. If it was an allied unit that previously had keywords, it gains +" + m_spellEffect + "/+" + m_spellEffect + " (+" + GetSpellValue() + "/+" + GetSpellValue() + ").\n" + GetModifiedBySpellPowerString();
        }
        else
        {
            return "Target non-elite unit loses all keywords. If it was an allied unit that previously had keywords, it gains +" + m_spellEffect + "/+" + m_spellEffect + ".\n" + GetModifiedBySpellPowerString();
        }
    }

    public override bool IsValidToPlay(GameUnit targetUnit)
    {
        return base.IsValidToPlay(targetUnit) && !GameHelper.IsBossOrElite(targetUnit);
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        if (targetUnit.GetTeam() == Team.Player)
        {
            if (targetUnit.GetKeywordHolderForRead().GetNumKeywords() > 0)
            {
                targetUnit.AddStats(GetSpellValue(), GetSpellValue());
            }
        }

        targetUnit.GetKeywordHolderForRead().RemoveAllKeywords();
    }
}