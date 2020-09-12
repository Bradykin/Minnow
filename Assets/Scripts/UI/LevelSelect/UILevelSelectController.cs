using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;
using UnityEngine.UI;

public class UILevelSelectController : Singleton<UILevelSelectController>
{
    //Store the level here

    public Text m_nameText;
    public Text m_difficultyText;
    public Text m_chaosVal;
    public Text m_chaosText;

    void Update()
    {
        m_nameText.text = "Level name";
        m_difficultyText.text = UIHelper.GetDifficultyText();
        m_difficultyText.color = UIHelper.GetDifficultyTextColor();

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

    public void SetSelectedLevel()
    {

    }
}
