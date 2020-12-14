using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLegionOfBladesCard : GameCardSpellBase
{
    public ContentLegionOfBladesCard()
    {
        m_name = "Legion of Blades";
        m_desc = "Discard your hand, and gain that many shivs.";
        m_targetType = Target.None;
        m_cost = 1;
        m_rarity = GameRarity.Rare;

        m_keywordHolder.AddKeyword(new GameShivKeyword());

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.Shiv);
        m_tags.AddTag(GameTag.TagType.MagicPower);
        m_tags.AddTag(GameTag.TagType.Spellcraft);
        m_tags.AddTag(GameTag.TagType.Knowledgeable);
        m_tags.AddTag(GameTag.TagType.DamageSpell);

        m_audioCategory = AudioHelper.SpellAudioCategory.Buff;
    }

    public override void PlayCard()
    {
        if (!IsValidToPlay())
        {
            return;
        }

        base.PlayCard();

        GamePlayer player = GameHelper.GetPlayer();
        int playerHandSize = player.m_hand.Count;
        player.DiscardHand();
        for (int i = 0; i < playerHandSize; i++)
        {
            player.AddCardToHand(new ContentShivCard(), false);
        }
    }
}
