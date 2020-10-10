using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;
using UnityEngine.UI;

public class UILevelSelectController : Singleton<UILevelSelectController>
{
    public GameMap m_curMap;

    public Text m_nameText;
    public Text m_descText;
    public GameObject m_infoObj;
    public Text m_difficultyText;
    public Text m_chaosVal;
    public Text m_chaosText;
    public Text m_chaosTitleText;

    public GameObject m_startGameButton;

    void Update()
    {
        if (m_curMap == null)
        {
            m_infoObj.SetActive(false);

            m_chaosVal.text = "";
            m_chaosText.text = "";
            m_chaosTitleText.text = "";

            m_nameText.text = "Select a Level";
            m_difficultyText.text = "";

            m_startGameButton.SetActive(false);
        }
        else
        {
            m_infoObj.SetActive(true);

            m_chaosTitleText.text = "Chaos";
            m_nameText.text = m_curMap.m_name;
            m_descText.text = m_curMap.m_desc;
            m_difficultyText.text = UIHelper.GetDifficultyText(m_curMap.m_difficulty);
            m_difficultyText.color = UIHelper.GetDifficultyTextColor(m_curMap.m_difficulty);

            m_chaosVal.text = "" + Globals.m_curChaos;
            m_chaosText.text = UIHelper.GetChaosDesc(Globals.m_curChaos);            

            m_startGameButton.SetActive(true);
        }
    }

    public bool HasLevelSelected()
    {
        return m_curMap != null;
    }

    public void SetSelectedLevel(GameMap newMap)
    {
        m_curMap = newMap;
    }
}
