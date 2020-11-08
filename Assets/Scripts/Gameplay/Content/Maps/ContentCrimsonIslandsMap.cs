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
            //AddMapEvent(new ContentDrySeasonMapEvent(), 2); //nmartino - Needs High/Low tide map events.
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
