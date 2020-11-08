using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLakesideMap : GameMap
{
    public ContentLakesideMap()
    {
        m_name = "Lakeside";
        m_desc = "A strong chokepoint makes this an excellent learning experience!";

        m_difficulty = MapDifficulty.Introduction;

        m_id = 0;

        m_spawnCrystals = false;

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
