﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UILevelSelectChaosButton : UIElementBase
    , IPointerClickHandler
{
    public GameObject m_holder;

    public bool m_isIncrease;

    void Update()
    {
        m_holder.SetActive(IsActive());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioSFXController.Instance.PlaySFX(AudioHelper.UIClick);

        if (!IsActive())
        {
            return;
        }

        if (m_isIncrease)
        {
            Globals.m_curChaos++;
        }
        else
        {
            Globals.m_curChaos--;
        }


        if (PlayerDataManager.PlayerAccountData.m_mapChaosUIAutoset.ContainsKey(UILevelSelectController.Instance.m_curMap.m_id))
        {
            PlayerDataManager.PlayerAccountData.m_mapChaosUIAutoset[UILevelSelectController.Instance.m_curMap.m_id] = Globals.m_curChaos;
        }
        else
        {
            PlayerDataManager.PlayerAccountData.m_mapChaosUIAutoset.Add(UILevelSelectController.Instance.m_curMap.m_id, Globals.m_curChaos);
        }

        HandleTooltip();
    }

    private bool IsActive()
    {
        if (!UILevelSelectController.Instance.HasLevelSelected())
        {
            return false;
        }

        if (m_isIncrease && Globals.m_curChaos == Constants.MaxChaos)
        {
            return false;
        }

        if (!m_isIncrease && Globals.m_curChaos == 1)
        {
            return false;
        }

        if (UILevelSelectController.Instance.m_curMap.m_difficulty == MapDifficulty.Introduction)
        {
            return false;
        }

        if (!Constants.UnlockAllContent && m_isIncrease && 
            (!PlayerDataManager.PlayerAccountData.m_mapChaosLevels.ContainsKey(UILevelSelectController.Instance.m_curMap.m_id) || 
            PlayerDataManager.PlayerAccountData.m_mapChaosLevels[UILevelSelectController.Instance.m_curMap.m_id] < Globals.m_curChaos))
        {
            return false;
        }

        return true;
    }

    public override void HandleTooltip()
    {
        //Left as stub
    }
}
