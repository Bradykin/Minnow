using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentAssassinationContractCard : GameCardSpellBase
{
    public ContentAssassinationContractCard()
    {
        m_name = "Assassination Contract";
        m_desc = "Add two shivs to your hand.";
        m_playDesc = "I'll take that mark...";
        m_targetType = Target.None;
        m_cost = 2;
        m_rarity = GameRarity.Common;

        SetupBasicData();
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

        player.AddCardToHand(new ContentShivCard(), false);
        player.AddCardToHand(new ContentShivCard(), false);
    }
}
