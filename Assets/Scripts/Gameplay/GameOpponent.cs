using Game.Util;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameOpponent : ITurns
{
    public List<GameEnemyUnit> m_controlledEntities { get; private set; }

    private List<GameEnemyUnit> m_secondTryAIControlledEntities;

    public List<GameSpawnPoint> m_spawnPoints { get; private set; }

    public GameOpponent()
    {
        m_controlledEntities = new List<GameEnemyUnit>();
        m_secondTryAIControlledEntities = new List<GameEnemyUnit>();
        m_spawnPoints = new List<GameSpawnPoint>();
    }

    public void LateInit()
    {
        //Debug.Log("GameOpponent LateInit");

        HandleSpawn();
    }

    public void AddControlledEntity(GameEnemyUnit toAdd)
    {
        m_controlledEntities.Add(toAdd);
    }

    public void RemoveControlledEntity(GameEnemyUnit toRemove)
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
        List<GameEnemyUnit> entities = new List<GameEnemyUnit>();
        entities.AddRange(m_controlledEntities);

        GameTile measureTo = GameHelper.GetPlayer().Castle.GetGameTile();

        while (entities.Count > 0)
        {
            GameEnemyUnit entity = entities.OrderBy(e => Vector3.Distance(e.GetWorldTile().transform.position, measureTo.GetWorldTile().transform.position)).First();

            int curAP = entity.GetCurStamina();
            yield return FactoryManager.Instance.StartCoroutine(entity.TakeTurn());

            entities.Remove(entity);

            if (Constants.UseSteppedOutEnemyTurns && !entity.GetGameTile().IsInFog())
            {
                if (!entity.m_isDead && entity.GetGameTile() != null)
                {
                    measureTo = entity.GetGameTile();
                }
                //yield return new WaitForSeconds(0.25f);
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
                GameEnemyUnit newEnemyEntity;
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
