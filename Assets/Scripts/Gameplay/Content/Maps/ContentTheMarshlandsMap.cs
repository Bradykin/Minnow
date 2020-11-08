using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTheMarshlandsMap : GameMap
{
    public ContentTheMarshlandsMap()
    {
        m_name = "The Marshlands";
        m_desc = "Why anyone would live in this marsh, I do not know. Beware the changing tides.";

        m_difficulty = MapDifficulty.Medium;

        m_id = 9;

        m_playerUnlockLevel = 1;

        Init();
    }

    protected override void FillMapEvents()
    {
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.MapEvents))
        {
            AddMapEvent(new ContentMarshTideRiseEvent(1), 2);
            AddMapEvent(new ContentMarshTideLowerEvent(1), 3);
            AddMapEvent(new ContentMarshTideLowerEvent(2), 4);
            AddMapEvent(new ContentMarshTideRiseEvent(2), 6);
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
