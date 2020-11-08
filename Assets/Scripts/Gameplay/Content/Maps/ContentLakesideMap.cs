using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLakesideMap : GameMap
{
    public ContentLakesideMap()
    {
        m_name = "Lakeside";
        m_desc = "A strong chokepoint makes this an excellent learning experience!";

        m_difficulty = MapDifficulty.Introduction;

        m_id = 0;

        m_spawnCrystals = false;

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
        m_spawnPool.Add(new ContentAngryBirdEnemy(null));
        m_spawnPool.Add(new ContentDarkWarriorEnemy(null));
        m_spawnPool.Add(new ContentLichEnemy(null));
        m_spawnPool.Add(new ContentLizardmanEnemy(null));
        m_spawnPool.Add(new ContentMobolaEnemy(null));
        m_spawnPool.Add(new ContentOrcEnemy(null));
        m_spawnPool.Add(new ContentOrcShamanEnemy(null));
        m_spawnPool.Add(new ContentLavaRhinoUnit(null));
        m_spawnPool.Add(new ContentSlimeEnemy(null));
        m_spawnPool.Add(new ContentSnakeEnemy(null));
        m_spawnPool.Add(new ContentSpinnerEnemy(null));
        m_spawnPool.Add(new ContentToadEnemy(null));
        m_spawnPool.Add(new ContentWerewolfEnemy(null));
        m_spawnPool.Add(new ContentYetiEnemy(null));
        m_spawnPool.Add(new ContentShadeEnemy(null));
        m_spawnPool.Add(new ContentZombieEnemy(null));


        List<GameEnemyUnit> specificSpawnPool1 = new List<GameEnemyUnit>();
        specificSpawnPool1.Add(new ContentOrcEnemy(null));

        List<GameEnemyUnit> specificSpawnPool2 = new List<GameEnemyUnit>();
        specificSpawnPool2.Add(new ContentToadEnemy(null));

        List<GameEnemyUnit> specificSpawnPool3 = new List<GameEnemyUnit>();
        specificSpawnPool3.Add(new ContentSpinnerEnemy(null));

        m_specificSpawnPools.Add(specificSpawnPool1);
        m_specificSpawnPools.Add(specificSpawnPool2);
        m_specificSpawnPools.Add(specificSpawnPool3);
    }
}
