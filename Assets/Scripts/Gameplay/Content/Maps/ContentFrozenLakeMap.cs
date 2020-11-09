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

    protected override void FillSpawnPool()
    {
        m_totalEnemiesOnMap.Add(new ContentCharybdisEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentGriffonEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentValgulaEnemy(null));
        


        m_totalEnemiesOnMap.Add(new ContentAngryBirdEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentDarkWarriorEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentLichEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentLizardmanEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentMobolaEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentOrcEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentOrcShamanEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentLavaRhinoEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentSlimeEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentSnakeEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentLancerEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentToadEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentWerewolfEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentYetiEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentShadeEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentZombieEnemy(null));

        List<GameSpawnPoolData> defaultSpawnPoolDatas = new List<GameSpawnPoolData>();
        //Wave 1
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentSlimeEnemy(null), 1, 1, 1));

        //Wave 2
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentSlimeEnemy(null), 2, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentLancerEnemy(null), 2, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentToadEnemy(null), 2, 1, 1));

        //Wave 3
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentLancerEnemy(null), 3, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentOrcEnemy(null), 3, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentOrcShamanEnemy(null), 3, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentAngryBirdEnemy(null), 3, 1, 1));

        //Wave 4
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentLancerEnemy(null), 4, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentOrcEnemy(null), 4, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentOrcShamanEnemy(null), 4, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentAngryBirdEnemy(null), 4, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentShadeEnemy(null), 4, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentSnakeEnemy(null), 4, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentLavaRhinoEnemy(null), 4, 1, 0.5f));

        //Wave 5
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentLavaRhinoEnemy(null), 5, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentWerewolfEnemy(null), 5, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentYetiEnemy(null), 5, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentMobolaEnemy(null), 5, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentLizardmanEnemy(null), 5, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentZombieEnemy(null), 5, 1, 1));

        //Wave 6
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentLavaRhinoEnemy(null), 6, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentWerewolfEnemy(null), 6, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentYetiEnemy(null), 6, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentMobolaEnemy(null), 6, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentLizardmanEnemy(null), 6, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentZombieEnemy(null), 6, 1, 1));

        m_defaultSpawnPool = new GameSpawnPool(defaultSpawnPoolDatas);
        m_spawnPointSpawnPools.Add(new GameSpawnPool(defaultSpawnPoolDatas));
        m_spawnPointSpawnPools.Add(new GameSpawnPool(defaultSpawnPoolDatas));
        m_spawnPointSpawnPools.Add(new GameSpawnPool(defaultSpawnPoolDatas));
        m_spawnPointSpawnPools.Add(new GameSpawnPool(defaultSpawnPoolDatas));
        m_spawnPointSpawnPools.Add(new GameSpawnPool(defaultSpawnPoolDatas));
    }
}
