using Game.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameOpponent : ITurns
{
    public List<GameEnemyUnit> m_controlledUnits { get; private set; }

    private List<GameEnemyUnit> m_secondTryAIControlledUnits;

    public List<GameSpawnPoint> m_spawnPoints { get; private set; }

    public GameOpponent()
    {
        m_controlledUnits = new List<GameEnemyUnit>();
        m_secondTryAIControlledUnits = new List<GameEnemyUnit>();
        m_spawnPoints = new List<GameSpawnPoint>();
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

            if (Constants.UseSteppedOutEnemyTurns && !unit.GetGameTile().IsInFog())
            {
                yield return FactoryManager.Instance.StartCoroutine(unit.TakeTurn(true));
            }
            else
            {
                FactoryManager.Instance.StartCoroutine(unit.TakeTurn(false));
            }

            units.Remove(unit);

            if (Constants.UseSteppedOutEnemyTurns && !unit.GetGameTile().IsInFog())
            {
                if (!unit.m_isDead && unit.GetGameTile() != null)
                {
                    measureTo = unit.GetGameTile();
                }
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
        for (int i = 0; i < m_spawnPoints.Count; i++)
        {
            if (m_spawnPoints[i].m_tile.m_occupyingUnit != null)
            {
                continue;
            }

            if (!m_spawnPoints[i].m_tile.m_isFog && Constants.FogOfWar)
            {
                continue;
            }

            if (GameHelper.PercentChanceRoll(Constants.PercentChanceForMobToSpawn))
            {
                GameEnemyUnit newEnemyUnit;
                if (GameHelper.GetGameController().m_waveNum == Constants.FinalWaveNum && !WorldController.Instance.HasSpawnedBoss())
                {
                    newEnemyUnit = GameUnitFactory.GetRandomBossEnemy(this);
                    WorldController.Instance.SetHasSpawnedBoss(true);
                }
                else if (!WorldController.Instance.HasSpawnedEliteThisWave() && GameHelper.GetGameController().m_currentWaveTurn >= Constants.SpawnEliteWave && GameHelper.PercentChanceRoll(Constants.PercentChanceForEliteToSpawn))
                {
                    newEnemyUnit = GameUnitFactory.GetRandomEliteEnemy(this);
                    WorldController.Instance.SetHasSpawnedEliteThisWave(true);
                }
                else
                {
                    newEnemyUnit = GameUnitFactory.GetRandomEnemy(this, GameHelper.GetGameController().m_waveNum);
                }

                m_spawnPoints[i].m_tile.PlaceUnit(newEnemyUnit);
                m_controlledUnits.Add(newEnemyUnit);
            }
        }
    }
}
