using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRemovalIntermissionAction : GameActionIntermission
{
    public ContentRemovalIntermissionAction()
    {
        m_actionCost = 1;
        m_name = "Purge";
        m_desc = "Remove a card from your deck!";

        m_icon = UIHelper.GetIconIntermissionAction(m_name);
    }

    public override void Activate()
    {
        GamePlayer player = GameHelper.GetPlayer();

        UIDeckViewController.Instance.Init(player.m_deckBase.GetDeck(), UIDeckViewController.DeckViewType.Remove, "Remove a Card");

        SpendCost();
    }
}
