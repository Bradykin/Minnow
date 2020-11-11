using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCrimsonIslandsMap : GameMap
{
    public ContentCrimsonIslandsMap()
    {
        m_name = "Crimson Islands";
        m_desc = "A small set of islands form your natural base, but you'll want to expand to discover the secrets of the others.";

        m_difficulty = MapDifficulty.Medium;

        m_id = 4;

        m_playerUnlockLevel = 1;

        Init();
    }

    protected override void FillMapEvents()
    {
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.MapEvents))
        {
            AddMapEvent(new ContentDrySeasonMapEvent(0), 2);
            AddMapEvent(new ContentFloodingMapEvent(0), 3);
            AddMapEvent(new ContentDrySeasonMapEvent(0), 4);
            AddMapEvent(new ContentFloodingMapEvent(0), 5);
            AddMapEvent(new ContentDrySeasonMapEvent(0), 6);
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
