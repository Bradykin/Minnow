using Game.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameOpponent : ITurns
{
    public List<GameEnemyUnit> m_controlledUnits { get; private set; }

    public List<GameBuildingBase> m_monsterDens { get; private set; }
    public List<GameSpawnPoint> m_spawnPoints { get; private set; }

    public int EliteSpawnWaveModifier;

    public GameOpponent()
    {
        m_controlledUnits = new List<GameEnemyUnit>();
        m_spawnPoints = new List<GameSpawnPoint>();
        m_monsterDens = new List<GameBuildingBase>();
    }

    public void LateInit()
    {
        //Debug.Log("GameOpponent LateInit");

        HandleSpawn();
    }

    public void AddControlledUnit(GameEnemyUnit toAdd)
    {
        m_controlledUnits.Add(toAdd);
    }

    public void RemoveControlledUnit(GameEnemyUnit toRemove)
    {
        m_controlledUnits.Remove(toRemove);
    }

    //============================================================================================================//

    private void TakeUnitTurns()
    {
        FactoryManager.Instance.StartCoroutine(TakeUnitTurnsCoroutine());
    }

    private IEnumerator TakeUnitTurnsCoroutine()
    {
        List<GameEnemyUnit> units = new List<GameEnemyUnit>();
        units.AddRange(m_controlledUnits);

        GameTile measureTo = GameHelper.GetPlayer().Castle.GetGameTile();

        while (units.Count > 0)
        {
            GameEnemyUnit unit = units.OrderBy(e => Vector3.Distance(e.GetWorldTile().transform.position, measureTo.GetWorldTile().transform.position)).First();

            unit.m_AIGameEnemyUnit.SetupTurn();

            if (unit.m_AIGameEnemyUnit.UseSteppedOutTurn)
            {
                yield return FactoryManager.Instance.StartCoroutine(unit.m_AIGameEnemyUnit.TakeTurn(true));
            }
            else
            {
                FactoryManager.Instance.StartCoroutine(unit.m_AIGameEnemyUnit.TakeTurn(false));
            }

            units.Remove(unit);

            if (Constants.UseSteppedOutEnemyTurns && !unit.GetGameTile().m_isFog)
            {
                if (!unit.m_isDead && unit.GetGameTile() != null)
                {
                    measureTo = unit.GetGameTile();
                }
            }
        }

        if (Constants.UseSteppedOutEnemyTurns && GameHelper.GetPlayer().Castle != null)
        {
            UICameraController.Instance.SmoothCameraTransitionToGameObject(GameHelper.GetPlayer().Castle.GetWorldTile().gameObject);
            while (UICameraController.Instance.IsCameraSmoothing())
            {
                yield return null;
            }
        }

        WorldController.Instance.m_gameController.MoveToNextTurn();
    }

    public void StartTurn()
    {
        for (int i = 0; i < m_controlledUnits.Count; i++)
        {
            m_controlledUnits[i].StartTurn();
        }
        TakeUnitTurns();
    }

    public void EndTurn()
    {
        for (int i = 0; i < m_controlledUnits.Count; i++)
        {
            m_controlledUnits[i].EndTurn();
        }

        if (GameHelper.GetGameController().m_currentWaveTurn >= GameHelper.GetGameController().GetEndWaveTurn())
        {
            return;
        }

        HandleSpawn();
    }

    private void HandleSpawn()
    {
        //Generate number of enemies to spawn
        int numEnemiesToSpawn = GameHelper.GetGameController().GetCurMap().GetNumEnemiesToSpawn();

        List<GameTile> tilesAtFogEdge = WorldGridManager.Instance.GetFogBorderGameTiles();
        tilesAtFogEdge.Sort((a, b) => 1 - 2 * UnityEngine.Random.Range(0, 2));

        //handle spawning of bosses and elites
        if (GameHelper.GetGameController().m_currentWaveTurn <= Constants.SpawnBossTurn && GameHelper.GetGameController().m_waveNum == Constants.FinalWaveNum && !WorldController.Instance.HasSpawnedBoss())
        {
            GameEnemyUnit gameEnemyUnit = GameUnitFactory.GetRandomBossEnemy(this);
            SpawnAtEdgeOfFog(gameEnemyUnit, tilesAtFogEdge);
            WorldController.Instance.SetHasSpawnedBoss(true);
        }

        if (GameHelper.GetGameController().m_currentWaveTurn >= (EliteSpawnWaveModifier + Constants.SpawnEliteTurn) && !WorldController.Instance.HasSpawnedEliteThisWave())
        {
            GameEnemyUnit gameEnemyUnit = GameUnitFactory.GetRandomEliteEnemy(this);
            SpawnAtEdgeOfFog(gameEnemyUnit, tilesAtFogEdge);
            WorldController.Instance.SetHasSpawnedEliteThisWave(true);
            Debug.Log("SPAWN ELITE");
        }

        //Try spawning at any monster dens
        for (int i = 0; i < m_monsterDens.Count; i++)
        {
            numEnemiesToSpawn -= SpawnEnemiesAtMonsterDen(m_monsterDens[i]);
        }

        m_spawnPoints.Sort((a, b) => 1 - 2 * UnityEngine.Random.Range(0, 2));
        for (int i = 0; i < m_spawnPoints.Count; i++)
        {
            if (TrySpawnAtSpawnPoint(m_spawnPoints[i]))
            {
                numEnemiesToSpawn--;

                if (numEnemiesToSpawn <= 0)
                {
                    return;
                }
            }
        }

        //Try spawning at edge of fog
        for (int i = 0; i < numEnemiesToSpawn; i++)
        {
            //Spawn enemy in edge of fog
            GameEnemyUnit newEnemyUnit = GameUnitFactory.GetRandomEnemy(this, GameHelper.GetGameController().m_waveNum);
            SpawnAtEdgeOfFog(newEnemyUnit, tilesAtFogEdge);
        }
    }

    private int SpawnEnemiesAtMonsterDen(GameBuildingBase gameBuilding)
    {
        int numEnemiesSpawned = 0;
        int numEnemiesToTryAndSpawn = 3;

        List<GameTile> tiles = WorldGridManager.Instance.GetSurroundingGameTiles(gameBuilding.GetGameTile(), 1, 0);
        tiles.Sort((a, b) => -1 + UnityEngine.Random.Range(0, 2));

        for (int i = 0; i < tiles.Count; i++)
        {
            GameEnemyUnit newEnemyUnit = GameUnitFactory.GetRandomEnemy(this, GameHelper.GetGameController().m_waveNum);
            if (!tiles[i].IsPassable(newEnemyUnit, false))
            {
                continue;
            }

            tiles[i].PlaceUnit(newEnemyUnit);
            m_controlledUnits.Add(newEnemyUnit);
            numEnemiesSpawned++;

            if (numEnemiesSpawned >= numEnemiesToTryAndSpawn)
            {
                break;
            }
        }

        return numEnemiesSpawned;
    }

    private bool TrySpawnAtSpawnPoint(GameSpawnPoint spawnPoint)
    {
        if (spawnPoint.m_tile.m_occupyingUnit != null)
        {
            return false;
        }

        if (!spawnPoint.m_tile.m_isFog && Constants.FogOfWar)
        {
            return false;
        }

        if (GameHelper.PercentChanceRoll(Constants.PercentChanceForMobToSpawn))
        {
            GameEnemyUnit newEnemyUnit = GameUnitFactory.GetRandomEnemy(this, GameHelper.GetGameController().m_waveNum);

            spawnPoint.m_tile.PlaceUnit(newEnemyUnit);
            m_controlledUnits.Add(newEnemyUnit);
            //Debug.Log("SPAWN " + newEnemyUnit + " AT SPAWN POINT");
            return true;
        }

        return false;
    }

    private void SpawnAtEdgeOfFog(GameEnemyUnit newEnemyUnit, List<GameTile> tilesAtFogEdge)
    {
        while (tilesAtFogEdge.Count > 0)
        {
            int curTileIndex = UnityEngine.Random.Range(0, tilesAtFogEdge.Count);

            if (!tilesAtFogEdge[curTileIndex].IsOccupied() && tilesAtFogEdge[curTileIndex].GetTerrain().IsPassable(newEnemyUnit))
            {
                tilesAtFogEdge[curTileIndex].PlaceUnit(newEnemyUnit);
                m_controlledUnits.Add(newEnemyUnit);
                //Debug.Log("SPAWN " + newEnemyUnit + " AT FOG EDGE");
                return;
            }
        }

        Debug.LogError("Spawn at Edge of fog failed to find any position to spawn in");
    }
}
