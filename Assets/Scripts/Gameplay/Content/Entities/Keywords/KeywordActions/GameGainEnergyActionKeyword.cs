using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGainEnergyActionKeyword : GameAction
{
    public GameGainEnergyActionKeyword()
    {
        m_name = "Gain Energy";
        m_desc = "Gain 1 energy.";
    }

    public override void DoAction()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        player.AddEnergy(1);
    }
}
