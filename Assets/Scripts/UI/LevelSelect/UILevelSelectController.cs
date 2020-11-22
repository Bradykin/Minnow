using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;
using UnityEngine.UI;
using TMPro;

public class UILevelSelectController : Singleton<UILevelSelectController>, IReset
{
    public GameMap m_curMap;

    public TMP_Text m_nameText;
    public TMP_Text m_descText;
    public GameObject m_infoObj;
    public TMP_Text m_difficultyText;
    public TMP_Text m_chaosVal;
    public TMP_Text m_chaosText;
    public TMP_Text m_chaosTitleText;

    public GameObject m_startGameButton;

    public GameObject m_mainMenu;

    public GameObject m_starterCardSelection;
    public GameObject m_starterRelicSelection;

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

            m_mainMenu.SetActive(true);
        }
        else
        {
            m_infoObj.SetActive(true);

            m_nameText.text = m_curMap.GetName();
            m_descText.text = m_curMap.GetDesc();
            m_difficultyText.text = UIHelper.GetDifficultyText(m_curMap.m_difficulty);
            m_difficultyText.color = UIHelper.GetDifficultyTextColor(m_curMap.m_difficulty);

            if (m_curMap.m_difficulty == MapDifficulty.Introduction)
            {
                m_chaosTitleText.gameObject.SetActive(false);
                m_chaosVal.gameObject.SetActive(false);
                m_chaosText.gameObject.SetActive(false);
            }
            else
            {
                m_chaosTitleText.gameObject.SetActive(true);
                m_chaosVal.gameObject.SetActive(true);
                m_chaosText.gameObject.SetActive(true);

                m_chaosTitleText.text = "Chaos";
                m_chaosVal.text = "" + Globals.m_curChaos;
                m_chaosText.text = UIHelper.GetChaosDesc(Globals.m_curChaos);
            }          

            m_startGameButton.SetActive(true);
            m_mainMenu.SetActive(false);

            m_starterCardSelection.SetActive(GameMetaprogressionUnlocksDataManager.GetNumStarterCardsUnlocked() > 0);
            m_starterRelicSelection.SetActive(GameMetaprogressionUnlocksDataManager.GetNumStarterRelicsUnlocked() > 0);
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

    public void Activate()
    {
        UICameraController.Instance.transform.position = UICameraController.Instance.m_startingTransform;
    }

    public void Reset()
    {
        
    }
}
