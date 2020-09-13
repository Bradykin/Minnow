using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPurpleBeamCount : WorldElementBase
{
    public Text m_valText;

    public SpriteRenderer m_tintRenderer;
    public GameObject m_holder;

    void Update()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        if (Globals.m_purpleBeamCount == 0)
        {
            m_holder.SetActive(false);
        }
        else
        {
            m_holder.SetActive(true);
            m_valText.text = "" + Globals.m_purpleBeamCount;
        }
    }

    void OnMouseOver()
    {
        UIHelper.SetValidTintColor(m_tintRenderer, true);
        Globals.m_canScroll = false;
    }

    void OnMouseExit()
    {
        UIHelper.SetDefaultTintColor(m_tintRenderer);
        Globals.m_canScroll = true;
    }

    public override void HandleTooltip()
    {
        if (!m_holder.activeSelf)
        {
            return;
        }

        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Purple Beam Count", "This is your purple beam count.  Some cards add to it, some cards spend it."));
    }
}
