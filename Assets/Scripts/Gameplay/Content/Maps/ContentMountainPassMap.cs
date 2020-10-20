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

    protected override void FillSpawnPool()
    {
        m_spawnPool.Add(new ContentDarkWarriorEnemy(null));
        m_spawnPool.Add(new ContentLichEnemy(null));
        m_spawnPool.Add(new ContentMobolaEnemy(null));
        m_spawnPool.Add(new ContentOrcEnemy(null));
        m_spawnPool.Add(new ContentOrcShamanEnemy(null));
        m_spawnPool.Add(new ContentSlimeEnemy(null));
        m_spawnPool.Add(new ContentSnakeEnemy(null));
        m_spawnPool.Add(new ContentSpinnerEnemy(null));
        m_spawnPool.Add(new ContentToadEnemy(null));
        m_spawnPool.Add(new ContentWerewolfEnemy(null));
        m_spawnPool.Add(new ContentYetiEnemy(null));
    }
}
