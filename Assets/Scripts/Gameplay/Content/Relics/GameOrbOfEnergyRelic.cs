using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOrbOfEnergyRelic : GameRelic
{
    public GameOrbOfEnergyRelic()
    {
        m_name = "Orb of Energy";
        m_desc = "Increase max energy by 1.";

        LateInit();
    }
}
