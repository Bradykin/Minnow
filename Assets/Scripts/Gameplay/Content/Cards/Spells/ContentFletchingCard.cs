using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFletchingCard : GameCardSpellBase
{
    public ContentFletchingCard()
    {
        m_name = "Fletching";
        m_targetType = Target.None;
        m_cost = 1;
        m_rarity = GameRarity.Common;
        m_spellEffect = 8;

        SetupBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Range, 2);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.BuffSpell);

        m_onPlaySFX = AudioHelper.SmallBuff;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }
        return $"Allied ranged units get +{UIHelper.GetMagicPowerColoredValue(m_spellEffect + mpString)}/+0 until end of turn.";
    }

    public override void PlayCard()
    {
        if (!IsValidToPlay())
        {
            return;
        }

        base.PlayCard();

        GameHelper.GetPlayer().m_fletchingPowerIncrease += GetSpellValue();
    }
}
