using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCosmicPactCard : GameCardSpellBase
{
    public ContentCosmicPactCard()
    {
        m_name = "Cosmic Pact";
        m_desc = "For the rest of this wave, all spells in your current hand cost 1 less.";
        m_targetType = Target.None;
        m_cost = 3;
        m_rarity = GameRarity.Rare;
        m_shouldExile = true;

        SetupBasicData();

        m_tagHolder.AddPullTag(GameTagHolder.TagType.UtilitySpell);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Spellcraft);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.LowCost);

        m_onPlaySFX = AudioHelper.MiscEffect;
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

        for (int i = 0; i < player.m_hand.Count; i++)
        {
            if (!(player.m_hand[i] is GameCardSpellBase))
            {
                continue;
            }
            player.m_hand[i].SetTempCost(-1);
        }
    }
}