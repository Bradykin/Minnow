using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDreamCard : GameCardSpellBase
{
    public ContentDreamCard()
    {
        m_spellEffect = 2;

        m_name = "Dream";
        m_targetType = Target.None;
        m_cost = 0;
        m_rarity = GameRarity.Common;

        SetupBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Knowledgeable);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Spellcraft);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.LowCost);

        m_onPlaySFX = AudioHelper.MiscEffect;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return $"Draw {UIHelper.GetMagicPowerColoredValue(m_spellEffect + mpString)} cards.";
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

        player.DrawCards(GetSpellValue());
    }
}