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
        m_cost = 1;
        m_rarity = GameRarity.Common;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.Knowledgeable);
        m_tags.AddTag(GameTag.TagType.Spellcraft);
        m_tags.AddTag(GameTag.TagType.LowCost);

        m_audioCategory = AudioHelper.SpellAudioCategory.Buff;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return "Draw " + m_spellEffect + mpString + " cards.\n" + GetModifiedByMagicPowerString();
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