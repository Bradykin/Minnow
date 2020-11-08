using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFrozenLakeMap : GameMap
{
    public ContentFrozenLakeMap()
    {
        m_name = "Frozen Lake";
        m_desc = "Your base has natural defenses, but beware the ice! Cracks in the ice can break apart, and who knows what lays beneath...";

        m_difficulty = MapDifficulty.Hard;

        m_id = 7;

        Init();
    }

    protected override void FillMapEvents()
    {
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.MapEvents))
        {
            AddMapEvent(new ContentIcequakeEvent(0), 2);
            AddMapEvent(new ContentIcequakeEvent(0), 3);
            AddMapEvent(new ContentIcequakeEvent(0), 4);
            AddMapEvent(new ContentIcequakeEvent(0), 5);
            AddMapEvent(new ContentIcequakeEvent(0), 6);
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
