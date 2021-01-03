using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertPassMap : GameMap
{
    public ContentDesertPassMap()
    {
        m_name = "Desert Pass";
        m_desc = "Can you protect the passes?";

        m_disableUnfinished = true;

        m_difficulty = MapDifficulty.Medium;

        m_id = 2;

        Init();
    }

    public override int GetNumEnemiesToSpawn()
    {
        return 5;
    }

    protected override void FillMapEvents()
    {
        //Need to fill
    }

    protected override void FillExclusionCardPool()
    {

    }

    protected override void FillSpawnPool()
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