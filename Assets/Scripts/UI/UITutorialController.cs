using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

public class UITutorialController : Singleton<UITutorialController>
{
    public GameObject m_unitTutorial;
    public GameObject m_spellTutorial;
    public GameObject m_tileTutorial;

    public enum TutorialType
    {
        Unit,
        Spell,
        Tile
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            CloseTutorials();
        }
    }

    public void ShowTutorial(TutorialType tutorialType)
    {
        if (tutorialType == TutorialType.Unit)
        {
            m_unitTutorial.SetActive(true);
            PlayerDataManager.PlayerAccountData.m_unitTutorialComplete = true;
        }
        else if (tutorialType == TutorialType.Spell)
        {
            m_spellTutorial.SetActive(true);
            PlayerDataManager.PlayerAccountData.m_spellTutorialComplete = true;
        }
        else if (tutorialType == TutorialType.Tile)
        {
            m_tileTutorial.SetActive(true);
            PlayerDataManager.PlayerAccountData.m_tileTutorialComplete = true;
        }
    }

    public void CloseTutorials()
    {
        m_unitTutorial.SetActive(false);
        m_spellTutorial.SetActive(false);
        m_tileTutorial.SetActive(false);
    }
}
