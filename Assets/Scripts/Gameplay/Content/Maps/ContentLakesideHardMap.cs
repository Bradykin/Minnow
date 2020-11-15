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

        m_id = 1;

        Init();
    }

    protected override void FillMapEvents()
    {
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.MapEvents))
        {
            AddMapEvent(new ContentDrySeasonMapEvent(0), 2);
            AddMapEvent(new ContentRainRefillMapEvent(0), 3);
        }
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
