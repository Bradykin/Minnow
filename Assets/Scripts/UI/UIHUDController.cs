using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHUDController : MonoBehaviour
{
    public GameObject m_waveHUD;
    public GameObject m_intermissionHUD;
    public GameObject m_globalHUD;
    public GameObject m_levelSelectHUD;

    void Update()
    {
        if (GameHelper.IsInLevelBuilder())
        {
            m_intermissionHUD.SetActive(false);
            m_waveHUD.SetActive(false);
            m_globalHUD.SetActive(false);
            m_levelSelectHUD.SetActive(false);
            return;
        }
        if (GameHelper.IsInLevelSelect())
        {
            m_intermissionHUD.SetActive(false);
            m_waveHUD.SetActive(false);
            m_globalHUD.SetActive(false);
            m_levelSelectHUD.SetActive(true);
            return;
        }
        else
        {
            m_levelSelectHUD.SetActive(false);
        }

        if (Globals.m_inDeckView)
        {
            m_intermissionHUD.SetActive(false);
            m_waveHUD.SetActive(false);
            m_globalHUD.SetActive(false);
            m_levelSelectHUD.SetActive(false);
        }
        else
        {
            m_globalHUD.SetActive(true);

            if (Globals.m_inIntermission)
            {
                m_intermissionHUD.SetActive(true);
                m_waveHUD.SetActive(false);
            }
            else
            {
                m_intermissionHUD.SetActive(false);

                if (WorldController.Instance.m_gameController.m_currentTurn == WorldController.Instance.m_gameController.m_player)
                {
                    m_waveHUD.SetActive(true);
                }
                else
                {
                    m_waveHUD.SetActive(false);
                }
            }
        }
    }
}
