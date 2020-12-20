using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentAssassinationContractCard : GameCardSpellBase
{
    private int m_shivAmount = 3;
    
    public ContentAssassinationContractCard()
    {
        m_name = "Assassination Contract";
        m_desc = "Add " + m_shivAmount + " shivs to your hand.";
        m_targetType = Target.None;
        m_cost = 3;
        m_rarity = GameRarity.Common;

        m_keywordHolder.AddKeyword(new GameShivKeyword());

        SetupBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Shiv, 2);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Spellcraft);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.MagicPower);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.DamageSpell);

        m_audioCategory = AudioHelper.SpellAudioCategory.Buff;
    }

    public override void PlayCard()
    {
        if (!IsValidToPlay())
        {
            return;
        }

        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        base.PlayCard();

        for (int i = 0; i < m_shivAmount; i++)
        {
            player.AddCardToHand(new ContentShivCard(), false);
        }
    }
}
