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
        m_fogSpawningActive = false;

        Init();
    }

    protected override void FillSpawnPool()
    {
        m_totalEnemiesOnMap.Add(new ContentAngryBirdEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentDarkWarriorEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentImmortalBladeEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentImmortalBowEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentImmortalBannerEnemy(null));
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
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentToadEnemy(null), 2, 1, 0.5f));

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
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentLavaRhinoEnemy(null), 4, 1, 0.25f));

        //Wave 5
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentLavaRhinoEnemy(null), 5, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentWerewolfEnemy(null), 5, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentYetiEnemy(null), 5, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentMobolaEnemy(null), 5, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentLizardmanEnemy(null), 5, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentZombieEnemy(null), 5, 1, 0.5f));

        //Wave 6
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentLavaRhinoEnemy(null), 6, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentWerewolfEnemy(null), 6, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentYetiEnemy(null), 6, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentMobolaEnemy(null), 6, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentLizardmanEnemy(null), 6, 1, 1));
        defaultSpawnPoolDatas.Add(new GameSpawnPoolData(new ContentZombieEnemy(null), 6, 1, 0.5f));

        m_defaultSpawnPool = new GameSpawnPool(defaultSpawnPoolDatas);
    }

    public override bool TrySpawnBoss(List<GameTile> tilesAtFogEdge)
    {
        GameOpponent gameOpponent = GameHelper.GetOpponent();
        List<GameEnemyUnit> immortalEnemyUnits = new List<GameEnemyUnit>();
        immortalEnemyUnits.Add(new ContentImmortalBladeEnemy(null));
        immortalEnemyUnits.Add(new ContentImmortalBowEnemy(null));
        immortalEnemyUnits.Add(new ContentImmortalBannerEnemy(null));
        List<GameEnemyUnit> activeBossUnits = GameHelper.GetGameController().m_activeBossUnits;


        bool allSpawned = true;
        for (int k = 0; k < immortalEnemyUnits.Count; k++)
        {
            bool immortalIsSpawned = false;
            for (int i = 0; i < activeBossUnits.Count; i++)
            {
                if (activeBossUnits[i].GetType() == immortalEnemyUnits[k].GetType())
                {
                    immortalIsSpawned = true;
                    break;
                }
            }

            if (immortalIsSpawned)
            {
                continue;
            }
            
            if (GetFogSpawningActive())
            {
                if (GameHelper.GetGameController().m_currentTurnNumber <= Constants.SpawnBossTurn && GameHelper.GetCurrentWaveNum() == Constants.FinalWaveNum)
                {
                    GameEnemyUnit gameEnemyUnit = GameUnitFactory.GetEnemyUnitClone(immortalEnemyUnits[k]);
                    if (!gameOpponent.TryForceSpawnAtEdgeOfFog(gameEnemyUnit, tilesAtFogEdge))
                    {
                        allSpawned = false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < gameOpponent.m_spawnPoints.Count; i++)
                {
                    GameSpawnPoint temp = gameOpponent.m_spawnPoints[i];
                    int randomIndex = UnityEngine.Random.Range(i, gameOpponent.m_spawnPoints.Count);
                    gameOpponent.m_spawnPoints[i] = gameOpponent.m_spawnPoints[randomIndex];
                    gameOpponent.m_spawnPoints[randomIndex] = temp;
                }

                if (GameHelper.GetGameController().m_currentTurnNumber <= Constants.SpawnBossTurn && GameHelper.GetCurrentWaveNum() == Constants.FinalWaveNum)
                {
                    GameEnemyUnit gameEnemyUnit = GameUnitFactory.GetEnemyUnitClone(immortalEnemyUnits[k]);
                    bool immortalSpawned = false;
                    for (int i = 0; i < gameOpponent.m_spawnPoints.Count; i++)
                    {
                        if (!gameOpponent.m_spawnPoints[i].m_tile.IsPassable(gameEnemyUnit, false))
                        {
                            continue;
                        }

                        if (gameOpponent.TryForceSpawnAtSpawnPoint(gameEnemyUnit, gameOpponent.m_spawnPoints[i]))
                        {
                            immortalSpawned = true;
                            break;
                        }
                    }

                    if (!immortalSpawned)
                    {
                        allSpawned = false;
                    }
                }
            }
        }

        return allSpawned;
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
}
