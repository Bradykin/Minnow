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

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Knowledgeable);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Spellcraft);

        m_onPlaySFX = AudioHelper.MiscEffect;
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