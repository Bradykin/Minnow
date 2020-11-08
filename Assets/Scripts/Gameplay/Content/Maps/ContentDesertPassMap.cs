using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertPassMap : GameMap
{
    public ContentDesertPassMap()
    {
        m_name = "Desert Pass";
        m_desc = "Can you protect the passes?";

        m_difficulty = MapDifficulty.Medium;

        m_id = 2;

        m_playerUnlockLevel = 3;

        Init();
    }

    protected override void FillMapEvents()
    {
        //Need to fill
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
        m_spawnPool.Add(new ContentAngryBirdEnemy(null));
        m_spawnPool.Add(new ContentDarkWarriorEnemy(null));
        m_spawnPool.Add(new ContentLichEnemy(null));
        m_spawnPool.Add(new ContentLizardmanEnemy(null));
        m_spawnPool.Add(new ContentMobolaEnemy(null));
        m_spawnPool.Add(new ContentOrcEnemy(null));
        m_spawnPool.Add(new ContentLavaRhinoUnit(null));
        m_spawnPool.Add(new ContentSlimeEnemy(null));
        m_spawnPool.Add(new ContentShadeEnemy(null));
        m_spawnPool.Add(new ContentSnakeEnemy(null));
        m_spawnPool.Add(new ContentSpinnerEnemy(null));
        m_spawnPool.Add(new ContentToadEnemy(null));
        m_spawnPool.Add(new ContentWerewolfEnemy(null));
        m_spawnPool.Add(new ContentZombieEnemy(null));
        m_spawnPool.Add(new ContentOrcShamanEnemy(null));
        m_spawnPool.Add(new ContentYetiEnemy(null));
    }
}