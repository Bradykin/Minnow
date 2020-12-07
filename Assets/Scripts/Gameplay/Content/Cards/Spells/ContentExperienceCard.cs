using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentExperienceCard : GameCardSpellBase
{
    public ContentExperienceCard()
    {
        m_name = "Experience";
        m_targetType = Target.Ally;
        m_rarity = GameRarity.Uncommon;
        m_xSpell = true;
        m_shouldExile = true;

        m_spellEffect = 1;

        m_cost = 0;

        m_tags.AddTag(GameTag.TagType.BuffSpell);

        SetupBasicData();

        m_audioCategory = AudioHelper.SpellAudioCategory.Buff;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return "Target allied unit <b>permanently</b> gains +" + m_spellEffect + mpString + "X/+" + m_spellEffect + mpString + "X.\n" + GetModifiedByMagicPowerString();
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        int yVal = GameHelper.GetPlayer().GetXValue() * GetSpellValue();

        targetUnit.AddStats(yVal, yVal, true, true);

        base.PlayCard(targetUnit);
    }
}
