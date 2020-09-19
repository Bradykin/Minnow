using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGainResourceAction : GameAction
{
    private GameWallet m_toGain;

    public GameGainResourceAction(GameWallet toGain)
    {
        m_toGain = toGain;

        m_name = "Gain Resources";
        m_actionParamType = ActionParamType.GameWalletParam;

        if (toGain == null)
        {
            return;
        }

        m_desc = "Gain " + m_toGain.ToString();
    }

    public override void DoAction()
    {
        GamePlayer player = GameHelper.GetPlayer();
        if (player == null)
        {
            return;
        }

        player.m_wallet.AddResources(m_toGain);
    }

    public override string SaveToJson()
    {
        JsonActionData jsonData = new JsonActionData
        {
            name = m_name,
            gameWalletJsonValue = JsonUtility.ToJson(m_toGain)
        };

        var export = JsonUtility.ToJson(jsonData);

        return export;
    }

    public override void LoadFromJson(JsonActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
