using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnergy : WorldElementBase
{
    public Text m_countText;

    void Update()
    {
        GamePlayer player = WorldController.Instance.m_gameController.m_player;
        m_countText.text = player.m_curEnergy + "/" + player.m_maxEnergy;
    }

    public override void HandleTooltip()
    {
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Energy", "This is your current energy!  It is used to play cards, and refreshes every turn."));
    }
}
