using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIWallet : WorldElementBase
{
    public Text m_goldText;

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

        GameWallet playerWallet = player.m_wallet;

        m_goldText.text = "" + playerWallet.m_gold;
    }

    public override void HandleTooltip()
    {
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Resources", "These are your current resources; gain them in the wave stage and spend them in the intermission phase!"));
    }
}
