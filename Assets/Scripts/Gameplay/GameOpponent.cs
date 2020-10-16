using Game.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameOpponent : ITurns
{
    public List<GameEnemyUnit> m_controlledUnits { get; private set; }

    public List<GameMonsterDenSpawnPoint> m_monsterDenSpawnPoints { get; private set; }
    public List<GameSpawnPoint> m_spawnPoints { get; private set; }

    public GameOpponent()
    {
        m_controlledUnits = new List<GameEnemyUnit>();
        m_spawnPoints = new List<GameSpawnPoint>();
        m_monsterDenSpawnPoints = new List<GameMonsterDenSpawnPoint>();
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
        int numEnemiesToSpawn = 10;

        //handle spawning of bosses and elites
        if (GameHelper.GetGameController().m_waveNum == Constants.FinalWaveNum && !WorldController.Instance.HasSpawnedBoss())
        {
            GameEnemyUnit gameEnemyUnit = GameUnitFactory.GetRandomBossEnemy(this);
            SpawnAtEdgeOfFog(gameEnemyUnit);
            WorldController.Instance.HasSpawnedBoss();
        }

        //Try spawning at any monster dens
        m_monsterDenSpawnPoints.Sort((a, b) => 1 - 2 * UnityEngine.Random.Range(0, 2));
        for (int i = 0; i < m_monsterDenSpawnPoints.Count; i++)
        {
            if (!m_monsterDenSpawnPoints[i].IsMonsterDenActive)
            {
                continue;
            }

            if (TrySpawnAtSpawnPoint(m_monsterDenSpawnPoints[i]))
            {
                numEnemiesToSpawn--;

                if (numEnemiesToSpawn == 0)
                {
                    return;
                }
            }
        }

        m_spawnPoints.Sort((a, b) => 1 - 2 * UnityEngine.Random.Range(0, 2));
        for (int i = 0; i < m_spawnPoints.Count; i++)
        {
            if (TrySpawnAtSpawnPoint(m_spawnPoints[i]))
            {
                numEnemiesToSpawn--;

                if (numEnemiesToSpawn == 0)
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
            SpawnAtEdgeOfFog(newEnemyUnit);
        }
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

            return true;
        }

        return false;
    }

    private void SpawnAtEdgeOfFog(GameEnemyUnit gameEnemyUnit)
    {

    }
}
