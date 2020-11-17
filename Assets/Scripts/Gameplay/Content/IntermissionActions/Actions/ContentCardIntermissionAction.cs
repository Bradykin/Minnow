using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCardIntermissionAction : GameActionIntermission
{
    public ContentCardIntermissionAction()
    {
        m_actionCost = 2;
        m_name = "Gain a card";
        m_desc = "Gain a spell card from a random set of 3.";

        m_icon = UIHelper.GetIconIntermissionAction(m_name);
    }

    public override void Activate()
    {
        GameRarity rarity = GameCardFactory.GetRandomRarity();

        List<GameCard> exclusionCards = new List<GameCard>();
        GameCard cardOne = GameCardFactory.GetRandomStandardSpellCard(rarity);
        exclusionCards.Add(cardOne);
        GameCard cardTwo = GameCardFactory.GetRandomStandardSpellCard(rarity, exclusionCards);
        exclusionCards.Add(cardTwo);
        GameCard cardThree = GameCardFactory.GetRandomStandardSpellCard(rarity, exclusionCards);

        UICardSelectController.Instance.Init(cardOne, cardTwo, cardThree);

        SpendCost();
    }
}
