﻿using Game.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameOpponent : ITurns, ISave<JsonGameOpponentData>, ILoad<JsonGameOpponentData>
{
    public List<GameEnemyUnit> m_controlledUnits { get; private set; }

    public List<GameBuildingBase> m_monsterDens { get; private set; }
    public List<GameSpawnPoint> m_spawnPoints { get; private set; }

    private int m_eliteSpawnWaveModifier;
    private bool m_hasSpawnedEliteThisWave;
    private bool m_hasSpawnedBoss;

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

        GameTile measureTo = GameHelper.GetPlayer().GetCastleGameTile();

        while (units.Count > 0)
        {
            GameEnemyUnit unit = units.OrderBy(e => Vector3.Distance(e.GetWorldTile().transform.position, measureTo.GetWorldTile().transform.position)).First();

            if (unit.m_isDead)
            {
                units.Remove(unit);
                continue;
            }

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

            if (PlayerDataManager.PlayerAccountData.m_followEnemy && !unit.GetGameTile().m_isFog)
            {
                if (!unit.m_isDead && unit.GetGameTile() != null)
                {
                    measureTo = unit.GetGameTile();
                }
            }
        }

        if (PlayerDataManager.PlayerAccountData.m_followEnemy && GameHelper.GetPlayer().GetCastleGameElement() != null)
        {
            UICameraController.Instance.SmoothCameraTransitionToGameObject(GameHelper.GetPlayer().GetCastleWorldTile().gameObject);
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

        if (GameHelper.GetGameController().m_currentTurnNumber >= GameHelper.GetGameController().GetEndWaveTurn())
        {
            return;
        }

        HandleSpawn();
    }

    public void OnBeginWave()
    {
        m_hasSpawnedEliteThisWave = false;
        m_eliteSpawnWaveModifier = UnityEngine.Random.Range(0, 3);
    }

    private void HandleSpawn()
    {
        //Generate number of enemies to spawn
        GameMap curMap = GameHelper.GetGameController().GetCurMap();
        int numEnemiesToSpawn = curMap.GetNumEnemiesToSpawn();
        bool fogSpawningActive = curMap.GetFogSpawningActive();

        List<GameTile> tilesAtFogEdge = WorldGridManager.Instance.GetFogBorderGameTiles();
        for (int i = 0; i < tilesAtFogEdge.Count; i++)
        {
            GameTile temp = tilesAtFogEdge[i];
            int randomIndex = UnityEngine.Random.Range(i, tilesAtFogEdge.Count);
            tilesAtFogEdge[i] = tilesAtFogEdge[randomIndex];
            tilesAtFogEdge[randomIndex] = temp;
        }

        //handle spawning of bosses and elites
        if (fogSpawningActive)
        {
            if (GameHelper.GetGameController().m_currentTurnNumber <= Constants.SpawnBossTurn && GameHelper.GetGameController().m_currentWaveNumber == Constants.FinalWaveNum && !m_hasSpawnedBoss)
            {
                GameEnemyUnit gameEnemyUnit = GameUnitFactory.GetRandomBossEnemy(this);
                SpawnAtEdgeOfFog(gameEnemyUnit, tilesAtFogEdge);
                m_hasSpawnedBoss = true;
            }

            if (GameHelper.GetGameController().m_currentTurnNumber >= (m_eliteSpawnWaveModifier + Constants.SpawnEliteTurn) && !m_hasSpawnedEliteThisWave)
            {
                GameEnemyUnit gameEnemyUnit = GameUnitFactory.GetRandomEliteEnemy(this);
                SpawnAtEdgeOfFog(gameEnemyUnit, tilesAtFogEdge);
                m_hasSpawnedEliteThisWave = true;
            }
        }
        else
        {
            for (int i = 0; i < m_spawnPoints.Count; i++)
            {
                GameSpawnPoint temp = m_spawnPoints[i];
                int randomIndex = UnityEngine.Random.Range(i, m_spawnPoints.Count);
                m_spawnPoints[i] = m_spawnPoints[randomIndex];
                m_spawnPoints[randomIndex] = temp;
            }

            if (GameHelper.GetGameController().m_currentTurnNumber <= Constants.SpawnBossTurn && GameHelper.GetGameController().m_currentWaveNumber == Constants.FinalWaveNum && !m_hasSpawnedBoss)
            {
                GameEnemyUnit gameEnemyUnit = GameUnitFactory.GetRandomBossEnemy(this);
                for (int i = 0; i < m_spawnPoints.Count; i++)
                {
                    if (!m_spawnPoints[i].m_tile.IsPassable(gameEnemyUnit, false))
                    {
                        continue;
                    }
                    
                    if (TryForceSpawnAtSpawnPoint(gameEnemyUnit, m_spawnPoints[i]))
                    {
                        break;
                    }
                }
                m_hasSpawnedBoss = true;
            }

            if (GameHelper.GetGameController().m_currentTurnNumber >= (m_eliteSpawnWaveModifier + Constants.SpawnEliteTurn) && !m_hasSpawnedEliteThisWave)
            {
                GameEnemyUnit gameEnemyUnit = GameUnitFactory.GetRandomEliteEnemy(this);
                for (int i = 0; i < m_spawnPoints.Count; i++)
                {
                    if (!m_spawnPoints[i].m_tile.IsPassable(gameEnemyUnit, false))
                    {
                        continue;
                    }

                    if (TryForceSpawnAtSpawnPoint(gameEnemyUnit, m_spawnPoints[i]))
                    {
                        break;
                    }
                }
                m_hasSpawnedEliteThisWave = true;
            }
        }

        //Try spawning at any monster dens
        for (int i = 0; i < m_monsterDens.Count; i++)
        {
            numEnemiesToSpawn -= SpawnEnemiesAtMonsterDen(m_monsterDens[i]);
        }

        for (int i = 0; i < m_spawnPoints.Count; i++)
        {
            GameSpawnPoint temp = m_spawnPoints[i];
            int randomIndex = UnityEngine.Random.Range(i, m_spawnPoints.Count);
            m_spawnPoints[i] = m_spawnPoints[randomIndex];
            m_spawnPoints[randomIndex] = temp;
        }

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

        if (fogSpawningActive)
        {
            //Try spawning at edge of fog
            for (int i = 0; i < numEnemiesToSpawn; i++)
            {
                //Spawn enemy in edge of fog
                GameEnemyUnit newEnemyUnit = GameUnitFactory.GetRandomEnemy(this, GameHelper.GetGameController().m_currentWaveNumber);
                SpawnAtEdgeOfFog(newEnemyUnit, tilesAtFogEdge);
            }
        }
    }

    private int SpawnEnemiesAtMonsterDen(GameBuildingBase gameBuilding)
    {
        int numEnemiesSpawned = 0;
        int numEnemiesToTryAndSpawn = 3;

        List<GameTile> tiles = WorldGridManager.Instance.GetSurroundingGameTiles(gameBuilding.GetGameTile(), 1, 0);
        for (int i = 0; i < tiles.Count; i++)
        {
            GameTile temp = tiles[i];
            int randomIndex = UnityEngine.Random.Range(i, tiles.Count);
            tiles[i] = tiles[randomIndex];
            tiles[randomIndex] = temp;
        }

        for (int i = 0; i < tiles.Count; i++)
        {
            GameEnemyUnit newEnemyUnit = GameUnitFactory.GetRandomEnemy(this, GameHelper.GetGameController().m_currentWaveNumber);
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
            GameEnemyUnit newEnemyUnit = GameUnitFactory.GetRandomEnemyFromSpawnPoint(this, GameHelper.GetGameController().m_currentWaveNumber, spawnPoint);

            if (newEnemyUnit == null)
            {
                return false;
            }

            spawnPoint.m_tile.PlaceUnit(newEnemyUnit);
            m_controlledUnits.Add(newEnemyUnit);
            //Debug.Log("SPAWN " + newEnemyUnit + " AT SPAWN POINT");
            return true;
        }

        return false;
    }

    private bool TryForceSpawnAtSpawnPoint(GameEnemyUnit newEnemyUnit, GameSpawnPoint spawnPoint)
    {
        if (spawnPoint.m_tile.m_occupyingUnit != null)
        {
            return false;
        }

        if (!spawnPoint.m_tile.m_isFog && Constants.FogOfWar)
        {
            return false;
        }

        if (newEnemyUnit == null)
        {
            return false;
        }

        spawnPoint.m_tile.PlaceUnit(newEnemyUnit);
        m_controlledUnits.Add(newEnemyUnit);

        return true;
    }

    private void SpawnAtEdgeOfFog(GameEnemyUnit newEnemyUnit, List<GameTile> tilesAtFogEdge)
    {
        while (tilesAtFogEdge.Count > 0)
        {
            int curTileIndex = UnityEngine.Random.Range(0, tilesAtFogEdge.Count);

            if (!tilesAtFogEdge[curTileIndex].IsOccupied() && tilesAtFogEdge[curTileIndex].IsPassable(newEnemyUnit, false))
            {
                tilesAtFogEdge[curTileIndex].PlaceUnit(newEnemyUnit);
                m_controlledUnits.Add(newEnemyUnit);
                //Debug.Log("SPAWN " + newEnemyUnit + " AT FOG EDGE");
                return;
            }
        }

        Debug.LogError("Spawn at Edge of fog failed to find any position to spawn in");
    }

    //============================================================================================================//

    public JsonGameOpponentData SaveToJson()
    {
        JsonGameOpponentData jsonData = new JsonGameOpponentData
        {
            eliteSpawnWaveModifier = m_eliteSpawnWaveModifier,
            hasSpawnedEliteThisWave = m_hasSpawnedEliteThisWave,
            hasSpawnedBoss = m_hasSpawnedBoss
};

        return jsonData;
    }

    public void LoadFromJson(JsonGameOpponentData jsonData)
    {
        m_eliteSpawnWaveModifier = jsonData.eliteSpawnWaveModifier;
        m_hasSpawnedEliteThisWave = jsonData.hasSpawnedEliteThisWave;
        m_hasSpawnedBoss = jsonData.hasSpawnedBoss;
    }
}
