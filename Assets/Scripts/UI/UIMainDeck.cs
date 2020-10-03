﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIMainDeck : UIElementBase
    , IPointerClickHandler
{
    public Text m_countText;

    void Start()
    {
        m_stopScrolling = true;
    }
    
    void Update()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        m_countText.text = player.m_deckBase.Count() + "";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UIDeckViewController.Instance.Init(GameHelper.GetPlayer().m_deckBase.GetDeck(), UIDeckViewController.DeckViewType.View, "Base Deck");
    }

    public override void HandleTooltip()
    {
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Full Deck", "This is your full deck, including currently played units and exile spells."));
    }
}
