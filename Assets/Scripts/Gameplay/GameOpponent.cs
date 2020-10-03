using Game.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameOpponent : ITurns
{
    public List<GameEnemyEntity> m_controlledEntities { get; private set; }

    private List<GameEnemyEntity> m_secondTryAIControlledEntities;

    public List<GameSpawnPoint> m_spawnPoints { get; private set; }

    public GameOpponent()
    {
        m_controlledEntities = new List<GameEnemyEntity>();
        m_secondTryAIControlledEntities = new List<GameEnemyEntity>();
        m_spawnPoints = new List<GameSpawnPoint>();
    }

    public void LateInit()
    {
        //Debug.Log("GameOpponent LateInit");

        HandleSpawn();
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
        List<GameEnemyEntity> entities = new List<GameEnemyEntity>();
        entities.AddRange(m_controlledEntities);

        GameTile measureTo = GameHelper.GetPlayer().Castle.GetGameTile();

        while (entities.Count > 0)
        {
            GameEnemyEntity entity = entities.OrderBy(e => Vector3.Distance(e.GetWorldTile().transform.position, measureTo.GetWorldTile().transform.position)).First();

            if (Constants.UseSteppedOutEnemyTurns && !entity.GetGameTile().IsInFog())
            {
                yield return FactoryManager.Instance.StartCoroutine(entity.TakeTurn());
            }
            else
            {
                yield return FactoryManager.Instance.StartCoroutine(entity.TakeTurn());
            }

            entities.Remove(entity);

            if (Constants.UseSteppedOutEnemyTurns && !entity.GetGameTile().IsInFog())
            {
                if (!entity.m_isDead && entity.GetGameTile() != null)
                {
                    measureTo = entity.GetGameTile();
                }
            }
        }

        if (!Constants.UseSteppedOutEnemyTurns)
        {
            yield return new WaitForSeconds(1.0f);
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
            if (m_spawnPoints[i].m_tile.m_occupyingEntity != null)
            {
                continue;
            }

            if (!m_spawnPoints[i].m_tile.m_isFog && Constants.FogOfWar)
            {
                continue;
            }

            if (GameHelper.PercentChanceRoll(Constants.PercentChanceForMobToSpawn))
            {
                GameEnemyEntity newEnemyEntity;
                if (GameHelper.GetGameController().m_waveNum == Constants.FinalWaveNum && !WorldController.Instance.HasSpawnedBoss())
                {
                    newEnemyEntity = GameEntityFactory.GetRandomBossEnemy(this);
                    WorldController.Instance.SetHasSpawnedBoss(true);
                }
                else if (!WorldController.Instance.HasSpawnedEliteThisWave() && GameHelper.GetGameController().m_currentWaveTurn >= Constants.SpawnEliteWave && GameHelper.PercentChanceRoll(Constants.PercentChanceForEliteToSpawn))
                {
                    newEnemyEntity = GameEntityFactory.GetRandomEliteEnemy(this);
                    WorldController.Instance.SetHasSpawnedEliteThisWave(true);
                }
                else
                {
                    newEnemyEntity = GameEntityFactory.GetRandomEnemy(this, GameHelper.GetGameController().m_waveNum);
                }

                m_spawnPoints[i].m_tile.PlaceEntity(newEnemyEntity);
                m_controlledEntities.Add(newEnemyEntity);
            }
        }
    }
}
