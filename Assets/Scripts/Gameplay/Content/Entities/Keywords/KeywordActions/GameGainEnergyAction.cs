using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGainEnergyAction : GameAction
{
    private int m_toGain;

    public GameGainEnergyAction(int toGain)
    {
        m_toGain = toGain;

        m_name = "Gain Energy";
        m_desc = "Gain " + m_toGain + " energy.";
    }

    public override void DoAction()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        player.AddEnergy(m_toGain);
    }
}
