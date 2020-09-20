using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIDeck : WorldElementBase
    , IPointerClickHandler
{
    public Text m_countText;

    void Start()
    {
        m_stopScrolling = true;
    }

    void Update()
    {
        m_countText.text = GameHelper.GetPlayer().m_curDeck.Count() + "";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GamePlayer player = GameHelper.GetPlayer();

        UIDeckViewController.Instance.Init(player.m_curDeck.GetDeck(), UIDeckViewController.DeckViewType.View);
    }

    public override void HandleTooltip()
    {
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Deck", "This is your deck!  When you run out of cards here, your discard will shuffle back in."));
    }
}
