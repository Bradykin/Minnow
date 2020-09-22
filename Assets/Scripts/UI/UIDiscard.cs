using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIDiscard : UIElementBase
    , IPointerClickHandler
{
    public Text m_countText;

    void Start()
    {
        m_stopScrolling = true;
    }

    void Update()
    {
        m_countText.text = GameHelper.GetPlayer().m_curDeck.DiscardCount() + "";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UIDeckViewController.Instance.Init(GameHelper.GetPlayer().m_curDeck.GetDiscard(), UIDeckViewController.DeckViewType.View);
    }

    public override void HandleTooltip()
    {
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Discard", "At the end of your turn, cards you played this turn will go here!  When you run out of cards in your deck, your discard will shuffle back in."));
    }
}
