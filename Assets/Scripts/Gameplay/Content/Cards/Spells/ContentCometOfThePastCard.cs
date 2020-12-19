using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCometOfThePastCard : GameCardSpellBase
{
    public ContentCometOfThePastCard()
    {
        m_spellEffect = 100;

        m_name = "Comet of the Past";
        m_targetType = Target.Enemy;
        m_cost = 0;
        m_rarity = GameRarity.Rare;
        m_shouldExile = true;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.HighCost);

        m_onPlaySFX = AudioHelper.LargeImpact;
    }

    public override string GetDesc()
    {
        string startingDesc = GetDamageDescString();

        return startingDesc;
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.GetHitBySpell(GetSpellValue(), this);
    }
}