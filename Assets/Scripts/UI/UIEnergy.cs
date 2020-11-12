using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnergy : UIElementBase
{
    public Text m_countText;

    void Start()
    {
        m_stopScrolling = true;
    }

    void Update()
    {
        GamePlayer player = GameHelper.GetPlayer();

        m_countText.text = player.m_curEnergy + "/" + player.GetMaxEnergy();
    }

    public override void HandleTooltip()
    {
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Energy", "This is your current energy!  It is used to play cards, and refreshes every turn."));
    }
}
