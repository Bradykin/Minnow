using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWolvenFangRelic : GameRelic
{
    public GameWolvenFangRelic()
    {
        m_name = "Wolven Fang";
        m_desc = "Increase power of all friendly entities by 1.";

        LateInit();
    }
}
