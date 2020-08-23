using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMysticRuneRelic : GameRelic
{
    public GameMysticRuneRelic()
    {
        m_name = "Mystic Rune";
        m_desc = "At the start of each turn draw + 2, randomize all energy costs from 0-3.";

        LateInit();
    }
}
