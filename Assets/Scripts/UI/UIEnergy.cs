﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnergy : WorldElementBase
{
    public Text m_titleText;
    public Text m_countText;
    public SpriteRenderer m_tintRenderer;

    void Update()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        if (Globals.m_inIntermission)
        {
            m_titleText.text = "Actions";
            m_countText.text = player.GetCurActions() + "/" + player.GetMaxActions();
        }
        else
        {
            m_titleText.text = "Energy";
            m_countText.text = player.m_curEnergy + "/" + player.GetMaxEnergy();
        }
    }

    void OnMouseOver()
    {
        UIHelper.SetValidTintColor(m_tintRenderer, true);
        Globals.m_canScroll = false;
    }

    void OnMouseExit()
    {
        UIHelper.SetDefaultTintColor(m_tintRenderer);
        Globals.m_canScroll = true;
    }

    public override void HandleTooltip()
    {
        if (Globals.m_inIntermission)
        {
            UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Actions", "Use these action points to take various actions during the intermission phase!"));
        }
        else
        {
            UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Energy", "This is your current energy!  It is used to play cards, and refreshes every turn."));
        }
    }
}
