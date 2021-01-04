using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UITurnCounter : UIElementBase
{
    public TMP_Text m_titleText;
    public TMP_Text m_countText;
    public TMP_Text m_chaosText;

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

        if (GameHelper.GetGameController().m_runStateType == RunStateType.Intermission)
        {
            m_titleText.text = "Intermission";
            m_countText.text = "Next wave: " + gameController.m_currentWaveNumber;
        }
        else
        {
            if (gameController.m_currentWaveNumber == Constants.FinalWaveNum)
            {
                m_titleText.text = "Final Wave";
                m_countText.text = "Beat the boss";
            }
            else
            {
                m_titleText.text = "Wave " + gameController.m_currentWaveNumber;

                int killCount = gameController.m_endWaveKillCount - gameController.m_curKillCount;
                if (killCount <= 0)
                {
                    killCount = 0;
                }

                m_countText.text = $"Kill {killCount} more.";
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
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Wave Counter", "After this many more kills, go to the intermission phase before the next wave!"));
        UIHelper.CreateChaosTooltipStack();
    }
}
