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

    void Update()
    {
        m_nameText.text = m_curLevel.mapName;
        m_difficultyText.text = UIHelper.GetDifficultyText((MapDifficulty)(m_curLevel.mapDifficulty));
        m_difficultyText.color = UIHelper.GetDifficultyTextColor((MapDifficulty)(m_curLevel.mapDifficulty));

        m_chaosVal.text = "" + Globals.m_curChaos;
        m_chaosText.text = ""; //Reset the text
        for (int i = 1; i <= Globals.m_curChaos; i++)
        {
            m_chaosText.text += UIHelper.GetChaosDesc(i) + "\n";
        }
    }

    public bool HasLevelSelected()
    {
        return true;
    }

    public void SetSelectedLevel(JsonMapMetaData newLevel)
    {
        m_curLevel = newLevel;
    }
}
