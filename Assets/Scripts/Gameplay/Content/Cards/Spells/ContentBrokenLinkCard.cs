using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBrokenLinkCard : GameCardSpellBase
{
    public ContentBrokenLinkCard()
    {
        m_name = "Broken Link";
        m_targetType = Target.Enemy;
        m_rarity = GameRarity.Common;
        m_xSpell = true;

        m_cost = 0;
        m_spellEffect = 5;

        m_tags.AddTag(GameTag.TagType.HighCost);
        m_tags.AddTag(GameTag.TagType.DamageSpell);
        m_tags.AddTag(GameTag.TagType.MagicPower);

        SetupBasicData();

        m_audioCategory = AudioHelper.SpellAudioCategory.Damage;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return "Deal " + m_spellEffect + mpString + " damage X times.\n";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        int xVal = GameHelper.GetPlayer().GetXValue();

        base.PlayCard(targetUnit);

        for (int i = 0; i < xVal; i++)
        {
            targetUnit.GetHitBySpell(GetSpellValue(), this);
        }
    }
}
