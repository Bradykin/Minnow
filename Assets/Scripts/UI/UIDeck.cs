using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDeck : WorldElementBase
{
    public Text m_countText;

    public SpriteRenderer m_tintRenderer;

    void Update()
    {
        GameDeck deck = WorldController.Instance.m_gameController.m_player.m_curDeck;
        m_countText.text = deck.Count() + "";
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
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Deck", "This is your deck!  When you run out of cards here, your discard will shuffle back in."));
    }
}
