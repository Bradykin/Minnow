using UnityEngine;
using UnityEngine.UI;

public class UITurnCounter : UIElementBase
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
        GameController gameController = GameHelper.GetGameController();

        if (gameController == null)
        {
            return;
        }

        if (Globals.m_inIntermission)
        {
            m_titleText.text = "Intermission";
            m_countText.text = "Next wave: " + gameController.m_waveNum;
        }
        else
        {
            if (gameController.m_waveNum == Constants.FinalWaveNum)
            {
                m_titleText.text = "Final Wave";
                m_countText.text = "Beat the boss";
            }
            else
            {
                int turnNum = gameController.m_currentWaveTurn - 1;

                m_titleText.text = "Wave " + gameController.m_waveNum;
                if (turnNum < gameController.GetEndWaveTurn()-1)
                {
                    m_countText.text = gameController.GetEndWaveTurn() - turnNum + " turns remain.";
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
