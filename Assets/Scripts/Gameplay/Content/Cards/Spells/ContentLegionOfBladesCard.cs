using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLegionOfBladesCard : GameCardSpellBase
{
    private int m_goldGain = 10;

    public ContentLegionOfBladesCard()
    {
        m_name = "Legion of Blades";
        m_desc = "Discard your hand, and gain that many shivs. This turn, you gain " + m_goldGain + " gold whenever an enemy unit dies to a shiv.";
        m_playDesc = "The blades draw near...";
        m_targetType = Target.None;
        m_cost = 2;
        m_rarity = GameRarity.Rare;

        SetupBasicData();
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

        Globals.m_goldPerShivKill += m_goldGain;
    }
}
