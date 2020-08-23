using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWallet : WorldElementBase
{
    public Text m_goldText;
    public Text m_magicText;
    public Text m_brickText;

    void Update()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        GameWallet playerWallet = player.m_wallet;

        m_goldText.text = "Gold: " + playerWallet.m_gold;
        m_magicText.text = "Magic: " + playerWallet.m_magic;
        m_brickText.text = "Bricks: " + playerWallet.m_bricks;
    }

    public override void HandleTooltip()
    {
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Resources", "These are your current resources; gain them in the rush stage and spend them in the intermission phase!"));
    }
}
