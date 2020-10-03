using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMarkedForDeathCard : GameCardSpellBase
{   
    public ContentMarkedForDeathCard()
    {
        m_spellEffect = 2;

        m_name = "Marked for Death";
        m_targetType = Target.Enemy;
        m_cost = 3;
        m_rarity = GameRarity.Uncommon;
        m_shouldExile = true;

        m_keywordHolder.m_keywords.Add(new GameEnrageKeyword(null));
        m_keywordHolder.m_keywords.Add(new GameBrittleKeyword(-1));

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.DamageSpell);
        m_tags.AddTag(GameTag.TagType.Spellpower);
    }

    public override string GetDesc()
    {
        return "Target enemy unit gains '<b>Enrage</b>: Gain <b>Brittle</b>: " + GetSpellValue() + "'.\n" + GetModifiedBySpellPowerString();
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddKeyword(new GameEnrageKeyword(new GameGainBrittleAction(targetUnit, GetSpellValue())));
    }
}
