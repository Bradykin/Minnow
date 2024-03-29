﻿using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHUDController : Singleton<UIHUDController>
{
    public GameObject m_waveHUD;
    public GameObject m_intermissionHUD;
    public GameObject m_globalHUD;
    public GameObject m_levelSelectHUD;

    void Update()
    {
        if (GameHelper.IsInWinLoss())
        {
            m_intermissionHUD.SetActive(false);
            m_waveHUD.SetActive(false);
            m_globalHUD.SetActive(false);
            m_levelSelectHUD.SetActive(false);
            return;
        }

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

            if (GameHelper.GetGameController().m_runStateType == RunStateType.Intermission)
            {
                m_intermissionHUD.SetActive(true);
                m_waveHUD.SetActive(false);
            }
            else
            {
                m_intermissionHUD.SetActive(false);

                if (WorldController.Instance.m_gameController.CurrentActor == WorldController.Instance.m_gameController.m_player)
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
