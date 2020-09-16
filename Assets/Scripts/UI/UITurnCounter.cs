using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITurnCounter : WorldElementBase
{
    public Text m_titleText;
    public Text m_countText;
    public Text m_chaosText;

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
            m_titleText.text = "Intermission";
            m_countText.text = "Next wave: " + player.m_waveNum;
        }
        else
        {
            if (player.m_waveNum == Constants.FinalWaveNum)
            {
                m_titleText.text = "Final Wave";
                m_countText.text = "Beat the boss";
            }
            else
            {
                int turnNum = player.m_currentWaveTurn;
                if (WorldController.Instance.m_gameController.m_currentTurn == GameHelper.GetOpponent())
                {
                    turnNum--;
                }

                m_titleText.text = "Wave " + player.m_waveNum;
                if (turnNum < player.GetEndWaveTurn())
                {
                    m_countText.text = turnNum + "/" + player.GetEndWaveTurn();
                }
                else
                {
                    m_countText.text = "Final turn";
                }
            }
        }

        if (Globals.m_curChaos == 0)
        {
            m_chaosText.text = "";
        }
        else
        {
            m_chaosText.text = "Chaos: " + Globals.m_curChaos;
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

    void OnMouseDown()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        UIDeckViewController.Instance.Init(player.m_deckBase.GetDeck(), UIDeckViewController.DeckViewType.View);
    }

    public override void HandleTooltip()
    {
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Wave Counter", "After this many turns, go to the intermission phase before the next wave!\n\nClick to see your full deck."));
        UIHelper.CreateChaosTooltipStack();
    }
}
