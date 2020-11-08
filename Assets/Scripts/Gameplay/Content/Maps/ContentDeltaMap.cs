using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDeltaMap : GameMap
{
    public ContentDeltaMap()
    {
        m_name = "Delta";
        m_desc = "An old region; many civilizations have left their remains behind along well trodden roads and in buried dunes.  Will you be different?";

        m_difficulty = MapDifficulty.Easy;

        m_id = 5;

        m_playerUnlockLevel = 3;

        Init();
    }

    protected override void FillMapEvents()
    {
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.MapEvents))
        {
            AddMapEvent(new ContentFloodingMapEvent(0), 2);
            AddMapEvent(new ContentFloodReceedingMapEvent(0), 4);

            AddMapEvent(new ContentFloodingMapEvent(0), 6);
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