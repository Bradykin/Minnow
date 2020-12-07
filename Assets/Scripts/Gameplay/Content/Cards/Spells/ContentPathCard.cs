using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPathCard : GameCardSpellBase
{
    public ContentPathCard()
    {
        m_name = "Path";
        m_targetType = Target.None;
        m_cost = 0;
        m_rarity = GameRarity.Uncommon;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.Knowledgeable);
        m_tags.AddTag(GameTag.TagType.Spellcraft);
        m_tags.AddTag(GameTag.TagType.LowCost);

        m_audioCategory = AudioHelper.SpellAudioCategory.Buff;
    }

    public override string GetDesc()
    {
        return "Draw a card";
    }

    public override void PlayCard()
    {
        if (!IsValidToPlay())
        {
            return;
        }

        base.PlayCard();

        GamePlayer player = GameHelper.GetPlayer();

        player.DrawCards(1);
    }
}