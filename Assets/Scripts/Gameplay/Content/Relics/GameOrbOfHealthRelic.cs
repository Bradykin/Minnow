using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOrbOfHealthRelic : GameRelic
{
    public GameOrbOfHealthRelic()
    {
        m_name = "Orb of Health";
        m_desc = "Increase the max health of all entities you control by 3!";

        LateInit();
    }
}
