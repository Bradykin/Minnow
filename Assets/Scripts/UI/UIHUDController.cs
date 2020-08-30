using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIHudController : MonoBehaviour
{
    public GameObject m_waveHUD;
    public GameObject m_intermissionHUD;

    void Update()
    {
        if (Globals.m_inIntermission)
        {
            m_intermissionHUD.SetActive(true);
            m_waveHUD.SetActive(false);
        }
        else
        {
            m_intermissionHUD.SetActive(false);
            m_waveHUD.SetActive(true);
        }
    }
}
