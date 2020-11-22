using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIActionsController : UIElementBase
{
    public TMP_Text m_countText;

    void Update()
    {
        GamePlayer player = GameHelper.GetPlayer();

        m_countText.text = player.GetCurActions() + "/" + player.GetMaxActions();
    }

    public override void HandleTooltip()
    {
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Actions", "Use these action points to take various actions during the intermission phase!"));
    }
}
