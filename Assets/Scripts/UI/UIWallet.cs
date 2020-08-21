using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWallet : MonoBehaviour
{
    public Text m_goldText;
    public Text m_magicText;
    public Text m_brickText;

    void Update()
    {
        GameWallet playerWallet = WorldController.Instance.m_gameController.m_player.m_wallet;

        m_goldText.text = "Gold: " + playerWallet.m_gold;
        m_magicText.text = "Magic: " + playerWallet.m_magic;
        m_brickText.text = "Bricks: " + playerWallet.m_bricks;
    }
}
