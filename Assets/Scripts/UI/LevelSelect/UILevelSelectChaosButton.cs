using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UILevelSelectChaosButton : WorldElementBase
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
        bool hasLevelSelected = UILevelSelectController.Instance.HasLevelSelected();
        bool isValidChaosChange = true;
        if (m_isIncrease && Globals.m_curChaos == Constants.MaxChaos)
        {
            isValidChaosChange = false;
        }

        if (!m_isIncrease && Globals.m_curChaos == 0)
        {
            isValidChaosChange = false;
        }

        return hasLevelSelected && isValidChaosChange;
    }

    public override void HandleTooltip()
    {
        UITooltipController.Instance.ClearTooltipStack();

        if (!IsActive())
        {
            return;
        }

        if (m_isIncrease)
        {
            UIHelper.CreateChaosTooltip(Globals.m_curChaos + 1);
        }
        else
        {
            UIHelper.CreateChaosTooltip(Globals.m_curChaos - 1);
        }
    }
}
