using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMountainPassMap : GameMap
{
    public ContentMountainPassMap()
    {
        m_name = "Mountain Pass";
        m_desc = "Terrain is on your side, but numbers are not.  Hold the pass!";

        m_difficulty = MapDifficulty.Easy;

        m_id = 6;
        m_fogSpawningActive = false;

        Init();
    }

    protected override void FillMapEvents()
    {
        //No events, left blank by default.  No Chaos on this map.
    }

    protected override void FillExclusionCardPool()
    {

    }

    protected override void FillEventPool()
    {
        FillBasicEventPool();
    }

    protected override void FillExclusionRelicPool()
    {

    }
}
