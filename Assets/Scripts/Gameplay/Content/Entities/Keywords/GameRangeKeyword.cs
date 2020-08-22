using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRangeKeyword : GameKeywordBase
{
    public int m_range;

    public GameRangeKeyword(int range)
    {
        m_range = range;

        m_name = "Ranged " + m_range;
        m_desc = "Can attack at range " + m_range + ".";
    }
}
