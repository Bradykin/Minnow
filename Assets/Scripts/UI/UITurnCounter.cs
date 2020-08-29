﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITurnCounter : WorldElementBase
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

        m_titleText.text = "Wave " + player.m_waveNum;
        m_countText.text = player.m_currentWaveTurn + "/" + player.m_currentWaveEndTurn;
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
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Wave Counter", "After this many turns, go to the intermission phase before the next wave!"));
    }
}