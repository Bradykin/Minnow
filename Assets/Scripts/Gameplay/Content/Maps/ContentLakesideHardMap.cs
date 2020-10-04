using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLakesideHardMap : GameMap
{
    public ContentLakesideHardMap()
    {
        m_name = "Lakeside Hard";
        m_desc = "Defend from 3 different angles in this expert map!";

        m_difficulty = MapDifficulty.Hard;

        AddMapEvent(new ContentDrySeasonMapEvent(), 2);

        m_id = 1;

        Init();
    }
}
