using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaskOfAgesRelic : GameRelic
{
    public GameMaskOfAgesRelic()
    {
        m_name = "Mask of Ages";
        m_desc = "At the start of each turn draw + 1";

        LateInit();
    }
}
