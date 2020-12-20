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

        m_tagHolder.AddPushTag(GameTagHolder.TagType.MagicPower);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.DamageSpell);

        SetupBasicData();

        m_onPlaySFX = AudioHelper.MagicEffect;
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
