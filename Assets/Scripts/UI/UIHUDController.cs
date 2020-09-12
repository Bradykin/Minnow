using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHUDController : MonoBehaviour
{
    public GameObject m_waveHUD;
    public GameObject m_intermissionHUD;
    public GameObject m_cardSelectHUD;
    public GameObject m_walletHUD;
    public GameObject m_relicsHUD;

    void Update()
    {
        if (GameHelper.IsInLevelSelect() || GameHelper.IsInLevelBuilder())
        {
            m_intermissionHUD.SetActive(false);
            m_waveHUD.SetActive(false);
            m_cardSelectHUD.SetActive(false);
            m_walletHUD.SetActive(false);
            m_relicsHUD.SetActive(false);
            return;
        }

        if (Globals.m_inDeckView)
        {
            m_intermissionHUD.SetActive(false);
            m_waveHUD.SetActive(false);
            m_cardSelectHUD.SetActive(false);
            m_walletHUD.SetActive(false);
            m_relicsHUD.SetActive(false);
        }
        else
        {
            m_walletHUD.SetActive(true);
            m_cardSelectHUD.SetActive(true);
            m_relicsHUD.SetActive(true);

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
