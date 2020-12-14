using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public override int GetNumEnemiesToSpawn()
    {
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.MapEvents) && GameHelper.GetGameController().m_currentWaveNumber % 2 == 0 && GameHelper.GetGameController().m_currentTurnNumber < 6)
        {
            return 4;
        }

        else
        {
            return 6;
        }
    }

    protected override void FillSpawnPool()
    {
        
        
        m_totalEnemiesOnMap.Add(new ContentAngryBirdEnemy(null));
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



        m_totalEnemiesOnMap.Add(new ContentGoblinWarriorEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentGoblinShamanEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentLancerEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentToadEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentOrcEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentOrcShamanEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentMetalCyclopsEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentMetalManticoreEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentSerpentineConstructEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentStoneflingerEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentGreenRockGiantEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentLavaRhinoEnemy(null));
        
        m_totalEnemiesOnMap.Add(new ContentImmortalSpearEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentImmortalBowEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentImmortalShieldEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentSkeletalPirateEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentSkeletalCaptainEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentZombieCrabEnemy(null));
        m_totalEnemiesOnMap.Add(new ContentZombieShipEnemy(null));

        //--------------------------------------------------------------------------------------------------------//

        List<GameSpawnPoolData> defaultSpawnPoolData = new List<GameSpawnPoolData>();
        //Wave 1
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentGoblinShamanEnemy(null), 1, 1, 0.33f));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentGoblinWarriorEnemy(null), 1, 1, 1));

        //Wave 2
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentGoblinShamanEnemy(null), 1, 1, 0.5f));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentGoblinWarriorEnemy(null), 1, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentLancerEnemy(null), 2, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentToadEnemy(null), 2, 1, 0.5f));

        //Wave 3
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentLancerEnemy(null), 3, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentOrcEnemy(null), 3, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentOrcShamanEnemy(null), 3, 1, 1));

        //Wave 4
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentOrcEnemy(null), 4, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentOrcShamanEnemy(null), 4, 1, 1));
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentStoneflingerEnemy(null), 4, 1, 0.2f));

        //Wave 5
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentStoneflingerEnemy(null), 5, 1, 1));

        //Wave 6
        defaultSpawnPoolData.Add(new GameSpawnPoolData(new ContentLavaRhinoEnemy(null), 6, 1, 1));

        //--------------------------------------------------------------------------------------------------------//

        m_defaultSpawnPool = new GameSpawnPool(defaultSpawnPoolData);
    }

    public override bool TrySpawnElite(List<GameTile> tilesAtFogEdge)
    {
        if (GameHelper.GetOpponent().m_hasSpawnedEliteThisWave)
        {
            return true;
        }

        List<GameTile> shipSpawnTiles = WorldGridManager.Instance.GetTilesWithEventMarker(0);

        GameTile tileToSpawn = shipSpawnTiles[Random.Range(0, shipSpawnTiles.Count)];

        GameEnemyUnit newEnemyUnit = GameUnitFactory.GetEnemyUnitClone(new ContentZombieShipEnemy(WorldController.Instance.m_gameController.m_gameOpponent));
        ((ContentZombieShipEnemy)newEnemyUnit).m_isEliteShip = true;
        tileToSpawn.PlaceUnit(newEnemyUnit);
        newEnemyUnit.OnSummon();
        WorldController.Instance.m_gameController.m_gameOpponent.AddControlledUnit(newEnemyUnit);

        return true;
    }

    public override bool TrySpawnBoss(List<GameTile> tilesAtFogEdge)
    {
        GameOpponent gameOpponent = GameHelper.GetOpponent();
        List<GameEnemyUnit> immortalEnemyUnits = new List<GameEnemyUnit>();
        immortalEnemyUnits.Add(new ContentImmortalSpearEnemy(null));
        immortalEnemyUnits.Add(new ContentImmortalBowEnemy(null));
        immortalEnemyUnits.Add(new ContentImmortalShieldEnemy(null));
        List<GameEnemyUnit> activeBossUnits = GameHelper.GetGameController().m_activeBossUnits;

        if (GameHelper.GetGameController().m_currentTurnNumber < Constants.SpawnBossTurn || GameHelper.GetCurrentWaveNum() != Constants.FinalWaveNum)
        {
            return false;
        }

        if (!gameOpponent.m_hasSpawnedBoss)
        {
            List<WorldTile> validTiles = new List<WorldTile>();
            List<WorldTile> tilesInRange = WorldGridManager.Instance.GetSurroundingWorldTiles(GameHelper.GetPlayer().GetCastleWorldTile(), Constants.GridSizeX, 12);

            for (int i = 0; i < tilesInRange.Count; i++)
            {
                if (!tilesInRange[i].GetGameTile().IsOccupied() && !tilesInRange[i].GetGameTile().HasBuilding() && tilesInRange[i].GetGameTile().IsPassable(null, false))
                {
                    validTiles.Add(tilesInRange[i]);
                }
            }

            while (true)
            {

                int r = UnityEngine.Random.Range(0, validTiles.Count);

                List<GameTile> surroundingValidSpawnTiles = WorldGridManager.Instance.GetSurroundingGameTiles(validTiles[r].GetGameTile(), 2, 2).Where(t => t.IsPassable(null, false)).ToList();

                if (surroundingValidSpawnTiles.Count < 2)
                {
                    continue;
                }

                for (int i = 0; i < surroundingValidSpawnTiles.Count; i++)
                {
                    GameTile temp = surroundingValidSpawnTiles[i];
                    int randomIndex = UnityEngine.Random.Range(i, surroundingValidSpawnTiles.Count);
                    surroundingValidSpawnTiles[i] = surroundingValidSpawnTiles[randomIndex];
                    surroundingValidSpawnTiles[randomIndex] = temp;
                }

                GameTile gameTile1 = validTiles[r].GetGameTile();
                GameTile gameTile2 = surroundingValidSpawnTiles[0];
                GameTile gameTile3 = surroundingValidSpawnTiles[1];

                gameOpponent.ForceSpawnNearPosition(new ContentImmortalSpearEnemy(gameOpponent), gameTile1);
                gameOpponent.ForceSpawnNearPosition(new ContentImmortalBowEnemy(gameOpponent), gameTile2);
                gameOpponent.ForceSpawnNearPosition(new ContentImmortalShieldEnemy(gameOpponent), gameTile3);
                gameTile1.GetWorldTile().ClearSurroundingFog(2);
                gameTile2.GetWorldTile().ClearSurroundingFog(2);
                gameTile3.GetWorldTile().ClearSurroundingFog(2);

                break;
            }

            UIHelper.CreateHUDNotification("Boss Arrived", "The Immortals has arrived to lead the legions of your enemies!");

            return true;
        }

        int numSpawned = 0;
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
            
            GameEnemyUnit gameEnemyUnit = GameUnitFactory.GetEnemyUnitClone(immortalEnemyUnits[k]);
            if (gameOpponent.TryForceSpawnAtEdgeOfFog(gameEnemyUnit, tilesAtFogEdge))
            {
                numSpawned++;
                continue;
            }

            //Failed to spawn at fog edge, instead spawn at a spawn point
            for (int i = 0; i < gameOpponent.m_spawnPoints.Count; i++)
            {
                GameSpawnPoint temp = gameOpponent.m_spawnPoints[i];
                int randomIndex = UnityEngine.Random.Range(i, gameOpponent.m_spawnPoints.Count);
                gameOpponent.m_spawnPoints[i] = gameOpponent.m_spawnPoints[randomIndex];
                gameOpponent.m_spawnPoints[randomIndex] = temp;
            }

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

            if (immortalSpawned)
            {
                numSpawned++;
                continue;
            }

            allSpawned = false;
        }

        if (numSpawned > 0)
        {
            UIHelper.CreateHUDNotification("Immortals Respawned", "The Immortals have respawned their fallen ranks");
        }

        return allSpawned;
    }

    protected override void FillMapEvents()
    {
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.MapEvents))
        {
            AddMapEvent(new ContentZombieFleetEvent(0), 1);
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

    public override List<GameTile> GetValidFogSpawningTiles()
    {
        return WorldGridManager.Instance.GetFogBorderGameTiles().Where(t => !t.HasEventMarker(1)).ToList();
    }
}
