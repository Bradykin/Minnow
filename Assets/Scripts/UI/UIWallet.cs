using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWallet : WorldElementBase
{
    public Text m_goldText;

    public Image m_tintImage;

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

    void OnMouseOver()
    {
        m_tintImage.color = UIHelper.GetValidTintColor(true);
        Globals.m_canScroll = false;
    }

    void OnMouseExit()
    {
        m_tintImage.color = UIHelper.GetDefaultTintColor();
        Globals.m_canScroll = true;
    }

    public override void HandleTooltip()
    {
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Resources", "These are your current resources; gain them in the wave stage and spend them in the intermission phase!"));
    }
}
