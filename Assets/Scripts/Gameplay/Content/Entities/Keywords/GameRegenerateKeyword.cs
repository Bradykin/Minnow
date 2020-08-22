using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRegenerateKeyword : GameKeywordBase
{
    public int m_regenVal;

    public GameRegenerateKeyword(int regenVal)
    {
        m_regenVal = regenVal;

        m_name = "Regenerate " + m_regenVal;
        m_desc = "Restores " + m_regenVal + " health at the start of each turn.";
    }
}
