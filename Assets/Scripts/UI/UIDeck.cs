using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDeck : WorldElementBase
{
    public Text m_countText;

    public Image m_tintImage;

    void Update()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        m_countText.text = player.m_curDeck.Count() + "";
    }

    void OnMouseOver()
    {
        m_tintImage.color = UIHelper.GetValidTintColor(true);
        Globals.m_canScroll = false;
    }

    void OnMouseExit()
    {
        m_tintImage.color = UIHelper.GetDefaultTintColor();
        Globals.m_canScroll = true;
    }

    void OnMouseDown()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        UIDeckViewController.Instance.Init(player.m_curDeck.GetDeck(), UIDeckViewController.DeckViewType.View);
    }

    public override void HandleTooltip()
    {
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Deck", "This is your deck!  When you run out of cards here, your discard will shuffle back in."));
    }
}
