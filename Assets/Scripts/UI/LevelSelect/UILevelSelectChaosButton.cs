using System.Collections;
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

        if (!m_isIncrease && Globals.m_curChaos == 0)
        {
            return false;
        }

        if (!Constants.CheatsOn && m_isIncrease && 
            (!GameMetaProgression.GamePlayerSaveData.m_mapChaosLevels.ContainsKey(UILevelSelectController.Instance.m_curMap.m_id) || 
            GameMetaProgression.GamePlayerSaveData.m_mapChaosLevels[UILevelSelectController.Instance.m_curMap.m_id] < Globals.m_curChaos))
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
