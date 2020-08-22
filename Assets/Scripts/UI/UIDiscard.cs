using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDiscard : WorldElementBase
{
    public Text m_countText;

    public SpriteRenderer m_tintRenderer;

    void Update()
    {
        GameDeck deck = WorldController.Instance.m_gameController.m_player.m_curDeck;
        m_countText.text = deck.DiscardCount() + "";
    }

    void OnMouseOver()
    {
        UIHelper.SetValidTintColor(m_tintRenderer, true);
    }

    void OnMouseExit()
    {
        UIHelper.SetDefaultTintColor(m_tintRenderer);
    }

    public override void HandleTooltip()
    {
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Discard", "After you play cards, they will go here!  When you run out of cards in your deck, your discard will shuffle back in."));
    }
}
