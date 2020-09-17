using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRainOfShivsCard : GameCardSpellBase
{
    public ContentRainOfShivsCard()
    {
        m_name = "Rain of Shivs";
        m_desc = "Add three shivs to your hand.";
        m_playDesc = "Three shivs are better then one!";
        m_targetType = Target.None;
        m_cost = 2;
        m_rarity = GameRarity.Uncommon;

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
        for (int i = 0; i < 3; i++)
        {
            player.AddCardToHand(GameCardFactory.GetCardClone(new ContentShivCard()), true);
        }
    }
}
