using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITurnCounter : WorldElementBase
{
    public Text m_titleText;
    public Text m_countText;
    public Text m_chaosText;

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

    public override void HandleTooltip()
    {
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Wave Counter", "After this many turns, go to the intermission phase before the next wave!"));
        UIHelper.CreateChaosTooltipStack();
    }
}
