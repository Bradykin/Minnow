using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnergy : WorldElementBase
{
    public Text m_countText;

    void Update()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        m_countText.text = player.m_curEnergy + "/" + player.GetMaxEnergy();
    }

    void OnMouseOver()
    {
        Globals.m_canScroll = false;
    }

    void OnMouseExit()
    {
        Globals.m_canScroll = true;
    }

    public override void HandleTooltip()
    {
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Energy", "This is your current energy!  It is used to play cards, and refreshes every turn."));
    }
}
