using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;
using UnityEngine.UI;

public class UILevelSelectController : Singleton<UILevelSelectController>
{
    public JsonMapMetaData m_curLevel;
    public bool m_levelBuilderSelected;

    public Text m_nameText;
    public Text m_difficultyText;
    public Text m_chaosVal;
    public Text m_chaosText;
    public Text m_chaosTitleText;

    public GameObject m_startGameButton;

    void Update()
    {
        if (m_levelBuilderSelected)
        {
            m_chaosVal.text = "";
            m_chaosText.text = "";
            m_chaosTitleText.text = "";

            m_nameText.text = "Level Builder";
            m_difficultyText.text = "";

            m_startGameButton.SetActive(true);
        } 
        else if (m_curLevel != null)
        {
            m_chaosTitleText.text = "Chaos";
            m_nameText.text = m_curLevel.mapName;
            m_difficultyText.text = UIHelper.GetDifficultyText((MapDifficulty)(m_curLevel.mapDifficulty));
            m_difficultyText.color = UIHelper.GetDifficultyTextColor((MapDifficulty)(m_curLevel.mapDifficulty));

            m_chaosVal.text = "" + Globals.m_curChaos;
            m_chaosText.text = ""; //Reset the text
            for (int i = 1; i <= Globals.m_curChaos; i++)
            {
                m_chaosText.text += UIHelper.GetChaosDesc(i) + "\n";
            }

            m_startGameButton.SetActive(true);
        }
        else if (m_curLevel == null)
        {
            m_chaosVal.text = "";
            m_chaosText.text = "";
            m_chaosTitleText.text = "";

            m_nameText.text = "Select a Level";
            m_difficultyText.text = "";

            m_startGameButton.SetActive(false);
        }
    }

    public bool HasLevelSelected()
    {
        return m_curLevel != null;
    }

    public void SetSelectedLevel(JsonMapMetaData newLevel)
    {
        m_curLevel = newLevel;
    }
}
