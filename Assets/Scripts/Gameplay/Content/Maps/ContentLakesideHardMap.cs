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

        AddMapEvent(new ContentDrySeasonMapEvent(), 2);

        m_id = 1;

        Init();
    }

    protected override void FillCardPool()
    {
        FillBasicCardPool();
    }

    protected override void FillEventPool()
    {
        FillBasicEventPool();
    }

    protected override void FillRelicPool()
    {
        FillBasicRelicPool();
    }

    protected override void FillSpawnPool()
    {
        m_spawnPool.Add(new ContentAngryBirdEnemy(null));
        m_spawnPool.Add(new ContentDarkWarriorEnemy(null));
        m_spawnPool.Add(new ContentLichEnemy(null));
        m_spawnPool.Add(new ContentLizardmanEnemy(null));
        m_spawnPool.Add(new ContentMobolaEnemy(null));
        m_spawnPool.Add(new ContentOrcEnemy(null));
        m_spawnPool.Add(new ContentOrcShamanEnemy(null));
        m_spawnPool.Add(new ContentSiegebreakerUnit(null));
        m_spawnPool.Add(new ContentShadeEnemy(null));
        m_spawnPool.Add(new ContentSlimeEnemy(null));
        m_spawnPool.Add(new ContentSnakeEnemy(null));
        m_spawnPool.Add(new ContentSpinnerEnemy(null));
        m_spawnPool.Add(new ContentToadEnemy(null));
        m_spawnPool.Add(new ContentWerewolfEnemy(null));
        m_spawnPool.Add(new ContentYetiEnemy(null));
        m_spawnPool.Add(new ContentZombieEnemy(null));
    }
}
