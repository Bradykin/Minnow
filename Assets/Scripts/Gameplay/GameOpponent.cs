﻿using Game.Util;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEditor.Rendering;
using UnityEngine;

public class GameOpponent : ITurns
{
    public List<GameEnemyEntity> m_controlledEntities { get; private set; }

    public List<GameSpawnPoint> m_spawnPoints { get; private set; }

    public GameOpponent()
    {
        m_controlledEntities = new List<GameEnemyEntity>();
        m_spawnPoints = new List<GameSpawnPoint>();
    }

    public void LateInit()
    {
        //Debug.Log("GameOpponent LateInit");
    }

    public void AddControlledEntity(GameEnemyEntity toAdd)
    {
        m_controlledEntities.Add(toAdd);
    }

    public void RemoveControlledEntity(GameEnemyEntity toRemove)
    {
        m_controlledEntities.Remove(toRemove);
    }

    //============================================================================================================//

    private void TakeEntityTurns()
    {
        FactoryManager.Instance.StartCoroutine(TakeEntityTurnsCoroutine());
    }

    private IEnumerator TakeEntityTurnsCoroutine()
    {
        for (int i = 0; i < m_controlledEntities.Count; i++)
        {
            if (Constants.UseSmartCameraEnemyTurns && !m_controlledEntities[i].m_curTile.m_isFog)
            {
                UICameraController.Instance.SnapToWorldElement(m_controlledEntities[i].m_curTile.m_curTile);
                yield return new WaitForSeconds(0.25f);
                m_controlledEntities[i].TakeTurn();
                UICameraController.Instance.SnapToWorldElement(m_controlledEntities[i].m_curTile.m_curTile);
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                m_controlledEntities[i].TakeTurn();
            }
        }
        WorldController.Instance.m_gameController.MoveToNextTurn();
    }

    public void StartTurn()
    {
        for (int i = 0; i < m_controlledEntities.Count; i++)
        {
            m_controlledEntities[i].StartTurn();
        }
        TakeEntityTurns();
    }

    public void EndTurn()
    {
        for (int i = 0; i < m_controlledEntities.Count; i++)
        {
            m_controlledEntities[i].EndTurn();
        }

        for (int i = 0; i < m_spawnPoints.Count; i++)
        {
            if (m_spawnPoints[i].m_tile.m_occupyingEntity != null)
                continue;

            if (GameHelper.PercentChanceRoll(Constants.PercentChanceForMobToSpawn))
            {
                GameEnemyEntity newEnemyEntity;
                if (GameHelper.GetPlayer().m_waveNum == Constants.FinalWaveNum && !WorldController.Instance.HasSpawnedBoss())
                {
                    newEnemyEntity = GameEntityFactory.GetRandomBossEnemy(this);
                    WorldController.Instance.SetHasSpawnedBoss(true);
                }
                else if (!WorldController.Instance.HasSpawnedEliteThisWave() && GameHelper.PercentChanceRoll(Constants.PercentChanceForEliteToSpawn))
                {
                    newEnemyEntity = GameEntityFactory.GetRandomEliteEnemy(this);
                    WorldController.Instance.SetHasSpawnedEliteThisWave(true);
                }
                else
                {
                    newEnemyEntity = GameEntityFactory.GetRandomEnemy(this, GameHelper.GetPlayer().m_waveNum);
                }

                m_spawnPoints[i].m_tile.PlaceEntity(newEnemyEntity);
                m_controlledEntities.Add(newEnemyEntity);
            }
        }
    }
}
