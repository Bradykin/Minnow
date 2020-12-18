using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIWallet : UIElementBase
{
    public TMP_Text m_goldText;

    void Start()
    {
        m_stopScrolling = true;
    }

    void Update()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        m_goldText.text = "" + player.GetGold();
    }

    public override void HandleTooltip()
    {
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Gold", "Gain gold in the wave stage and spend it in the intermission phase!"));
    }
}
