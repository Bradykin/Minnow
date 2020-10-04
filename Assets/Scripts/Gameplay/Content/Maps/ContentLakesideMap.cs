using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLakesideMap : GameMap
{
    public ContentLakesideMap()
    {
        m_name = "Lakeside";
        m_desc = "A strong chokepoint makes this an excellent learning experience!";

        m_difficulty = MapDifficulty.Easy;

        m_id = 0;

        Init();
    }
}
