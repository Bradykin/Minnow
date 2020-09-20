using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelSelectChaosButton : WorldElementBase
{
    public GameObject m_holder;
    public Image m_tintImage;

    public bool m_isIncrease;

    void Update()
    {
        m_holder.SetActive(IsActive());
    }

    void OnMouseDown()
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

    void OnMouseOver()
    {
        m_tintImage.color = UIHelper.GetValidTintColor(true);
    }

    void OnMouseExit()
    {
        m_tintImage.color = UIHelper.GetDefaultTintColor();
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
