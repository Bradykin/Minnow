using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSnowmeltMap : GameMap
{
    public ContentSnowmeltMap()
    {
        m_name = "Snowmelt";
        m_desc = "Enemies swarm; but an abundance of ruins should help to hold this frozen wasteland.";

        m_difficulty = MapDifficulty.Medium;

        m_id = 3;

        m_playerUnlockLevel = 2;

        Init();
    }

    protected override void FillMapEvents()
    {
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.MapEvents))
        {
            AddMapEvent(new ContentSnapFreezeMapEvent(0), 3);
            AddMapEvent(new ContentSnapThawMapEvent(0), 4);

            AddMapEvent(new ContentSnapFreezeMapEvent(0), 5);
            AddMapEvent(new ContentSnapThawMapEvent(0), 6);
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