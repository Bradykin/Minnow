using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILevelSelectChaosButton : WorldElementBase
{
    public GameObject m_holder;
    public SpriteRenderer m_tintRenderer;

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
        UIHelper.SetValidTintColor(m_tintRenderer, true);
    }

    void OnMouseExit()
    {
        UIHelper.SetDefaultTintColor(m_tintRenderer);
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
