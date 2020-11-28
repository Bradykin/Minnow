using Game.Util;
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

    public int m_eliteSpawnWaveModifier { get; private set; }
    public bool m_hasSpawnedEliteThisWave { get; private set; }
    public bool m_hasSpawnedBoss { get; private set; }

    public GameOpponent()
    {
        m_controlledUnits = new List<GameEnemyUnit>();
        m_spawnPoints = new List<GameSpawnPoint>();
        m_monsterDens = new List<GameBuildingBase>();
    }

    public void LateInit()
    {
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

    public void InformHasDied(GameUnit deadUnit, GameTile deathLocation)
    {
        for (int i = 0; i < m_controlledUnits.Count; i++)
        {
            if (m_controlledUnits[i] == deadUnit)
            {
                continue;
            }

            m_controlledUnits[i].OnOtherDie(deadUnit, deathLocation);
        }
    }

    //============================================================================================================//

    public void TriggerSpellcraft(GameCard.Target targetType, GameTile targetTile)
    {
        for (int i = 0; i < m_controlledUnits.Count; i++)
        {
            m_controlledUnits[i].SpellCast(targetType, targetTile);
        }
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
            units.RemoveAll(u => u == null || u.m_isDead);
            
            GameEnemyUnit unit = units.OrderBy(e => Vector3.Distance(e.GetWorldTile().transform.position, measureTo.GetWorldTile().transform.position)).First();

            unit.m_AIGameEnemyUnit.SetupTurn();

            if (unit.m_AIGameEnemyUnit.UseSteppedOutTurn)
            {
                yield return FactoryManager.Instance.StartCoroutine(unit.m_AIGameEnemyUnit.TakeTurnCoroutine());
            }
            else
            {
                unit.m_AIGameEnemyUnit.TakeTurnInstant();
            }

            units.Remove(unit);

            if (!unit.m_isDead && unit.GetGameTile() != null)
            {
                if (PlayerDataManager.PlayerAccountData.m_followEnemy && !unit.GetGameTile().m_isFog)
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

        if (WorldController.Instance.m_isInGame)
        {
            WorldController.Instance.m_gameController.MoveToNextTurn();
        }
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
        float enemyCapToSpawn = curMap.GetNumEnemiesToSpawn();
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
        m_hasSpawnedBoss = curMap.TrySpawnBoss(tilesAtFogEdge);
        m_hasSpawnedEliteThisWave = curMap.TrySpawnElite(tilesAtFogEdge);

        //Try spawning at any monster dens
        for (int i = 0; i < m_monsterDens.Count; i++)
        {
            TrySpawnEnemiesAtMonsterDen(m_monsterDens[i], ref enemyCapToSpawn);
            if (enemyCapToSpawn <= 0)
            {
                return;
            }
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
            if (TrySpawnAtSpawnPoint(m_spawnPoints[i], ref enemyCapToSpawn))
            {
                if (enemyCapToSpawn <= 0)
                {
                    return;
                }
            }
        }

        if (fogSpawningActive)
        {
            //Try spawning at edge of fog
            int numTries = 10;
            while (enemyCapToSpawn > 0 && tilesAtFogEdge.Count >= 0 && numTries > 0)
            {
                if (TrySpawnAtEdgeOfFog(tilesAtFogEdge, ref enemyCapToSpawn))
                {
                    numTries = 10;
                }
                else
                {
                    numTries--;
                }
            }
        }
    }

    private int TrySpawnEnemiesAtMonsterDen(GameBuildingBase gameBuilding, ref float enemyCapToSpawn)
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
            GameSpawnPoolData newSpawnPoolData = GameUnitFactory.GetRandomEnemyFromDefaultSpawnPool(this, GameHelper.GetCurrentWaveNum());
            GameEnemyUnit newEnemyUnit = GameUnitFactory.GetEnemyUnitClone(newSpawnPoolData.m_gameEnemy);

            if (!tiles[i].IsPassable(newEnemyUnit, false))
            {
                continue;
            }

            if (newSpawnPoolData.m_spawnWeight > enemyCapToSpawn)
            {
                continue;
            }
            enemyCapToSpawn -= newSpawnPoolData.m_spawnWeight;

            tiles[i].PlaceUnit(newEnemyUnit);
            newEnemyUnit.OnSummon();
            m_controlledUnits.Add(newEnemyUnit);
            numEnemiesSpawned++;

            if (numEnemiesSpawned >= numEnemiesToTryAndSpawn)
            {
                break;
            }
        }

        return numEnemiesSpawned;
    }

    private bool TrySpawnAtSpawnPoint(GameSpawnPoint spawnPoint, ref float enemyCapToSpawn)
    {
        if (spawnPoint.m_tile.GetOccupyingUnit() != null)
        {
            return false;
        }

        if (!spawnPoint.m_tile.m_isFog && Constants.FogOfWar)
        {
            return false;
        }

        if (GameHelper.PercentChanceRoll(Constants.PercentChanceForMobToSpawn))
        {
            GameSpawnPoolData newSpawnPoolData = GameUnitFactory.GetRandomEnemyFromSpawnPoint(this, GameHelper.GetCurrentWaveNum(), spawnPoint);

            if (newSpawnPoolData == null)
            {
                return false;
            }

            GameEnemyUnit newEnemyUnit = GameUnitFactory.GetEnemyUnitClone(newSpawnPoolData.m_gameEnemy);

            if (newEnemyUnit == null)
            {
                return false;
            }

            if (!spawnPoint.m_tile.IsPassable(newEnemyUnit, false))
            {
                return false;
            }

            if (newSpawnPoolData.m_spawnWeight > enemyCapToSpawn)
            {
                return false;
            }
            enemyCapToSpawn -= newSpawnPoolData.m_spawnWeight;

            spawnPoint.m_tile.PlaceUnit(newEnemyUnit);
            newEnemyUnit.OnSummon();
            m_controlledUnits.Add(newEnemyUnit);
            return true;
        }

        return false;
    }

    public bool TryForceSpawnAtSpawnPoint(GameEnemyUnit newEnemyUnit, GameSpawnPoint spawnPoint)
    {
        if (spawnPoint.m_tile.GetOccupyingUnit() != null)
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

        if (!spawnPoint.m_tile.IsPassable(newEnemyUnit, false))
        {
            return false;
        }

        spawnPoint.m_tile.PlaceUnit(newEnemyUnit);
        newEnemyUnit.OnSummon();
        m_controlledUnits.Add(newEnemyUnit);

        return true;
    }

    private bool TrySpawnAtEdgeOfFog(List<GameTile> tilesAtFogEdge, ref float enemyCapToSpawn)
    {
        GameSpawnPoolData newSpawnPoolData = GameUnitFactory.GetRandomEnemyFromDefaultSpawnPool(this, GameHelper.GetCurrentWaveNum());

        if (newSpawnPoolData == null)
        {
            return false;
        }

        GameEnemyUnit newEnemyUnit = GameUnitFactory.GetEnemyUnitClone(newSpawnPoolData.m_gameEnemy);

        if ((enemyCapToSpawn < newSpawnPoolData.m_spawnWeight) || (enemyCapToSpawn <= 0 && newSpawnPoolData.m_spawnWeight > 0))
        {
            return false;
        }
        enemyCapToSpawn -= newSpawnPoolData.m_spawnWeight;

        return TryForceSpawnAtEdgeOfFog(newEnemyUnit, tilesAtFogEdge);
    }

    public bool TryForceSpawnAtEdgeOfFog(GameEnemyUnit newEnemyUnit, List<GameTile> tilesAtFogEdge)
    {
        while (tilesAtFogEdge.Count > 0)
        {
            int curTileIndex = UnityEngine.Random.Range(0, tilesAtFogEdge.Count);

            if (tilesAtFogEdge[curTileIndex].IsOccupied())
            {
                tilesAtFogEdge.RemoveAt(curTileIndex);
                continue;
            }

            if (!tilesAtFogEdge[curTileIndex].IsPassable(newEnemyUnit, false))
            {
                tilesAtFogEdge.RemoveAt(curTileIndex);
                continue;
            }

            tilesAtFogEdge[curTileIndex].PlaceUnit(newEnemyUnit);
            newEnemyUnit.OnSummon();
            m_controlledUnits.Add(newEnemyUnit);
            return true;
        }

        Debug.Log("Spawn at Edge of fog failed to find any position to spawn in");
        return false;
    }

    public bool ForceSpawnNearPosition(GameEnemyUnit newEnemyUnit, GameTile tileToSpawnAtOrNear)
    {
        for (int i = 0; i < 5; i++)
        {
            List<GameTile> tilesAtRange = WorldGridManager.Instance.GetSurroundingGameTiles(tileToSpawnAtOrNear, i, Mathf.Min(i, 1)).Where(t => !t.IsOccupied() && t.IsPassable(newEnemyUnit, false)).ToList();

            if (tilesAtRange.Count == 0)
            {
                continue;
            }

            GameTile tileToSpawnAt = tilesAtRange[UnityEngine.Random.Range(0, tilesAtRange.Count)];
            tileToSpawnAt.PlaceUnit(newEnemyUnit);
            newEnemyUnit.OnSummon();
            m_controlledUnits.Add(newEnemyUnit);
            return true;
        }

        Debug.LogError("No positions able to spawn anywhere near tile at " + tileToSpawnAtOrNear.m_gridPosition);
        return false;
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
