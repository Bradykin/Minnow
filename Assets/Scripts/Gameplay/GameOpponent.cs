using Game.Util;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
        for (int i = 0; i < m_controlledEntities.Count; i++)
        {
            if (Constants.UseSmartCameraEnemyTurns && !m_controlledEntities[i].GetGameTile().m_isFog)
            {
                UICameraController.Instance.SnapToGameObject(m_controlledEntities[i].GetWorldTile().gameObject);
                int curAP = m_controlledEntities[i].GetCurAP();
                yield return new WaitForSeconds(0.25f);
                m_controlledEntities[i].TakeTurn();
                //UICameraController.Instance.SnapToWorldElement(m_controlledEntities[i].GetWorldTile());
                yield return new WaitForSeconds(0.5f);

                if (curAP == m_controlledEntities[i].GetCurAP())
                {
                    m_secondTryAIControlledEntities.Add(m_controlledEntities[i]);
                }
            }
            else
            {
                int curAP = m_controlledEntities[i].GetCurAP();

                m_controlledEntities[i].TakeTurn();

                if (curAP == m_controlledEntities[i].GetCurAP())
                {
                    m_secondTryAIControlledEntities.Add(m_controlledEntities[i]);
                }
            }
        }

        for (int i = 0; i < m_secondTryAIControlledEntities.Count; i++)
        {
            if (Constants.UseSmartCameraEnemyTurns && !m_secondTryAIControlledEntities[i].GetGameTile().m_isFog)
            {
                UICameraController.Instance.SnapToGameObject(m_secondTryAIControlledEntities[i].GetWorldTile().gameObject);
                yield return new WaitForSeconds(0.25f);
                m_secondTryAIControlledEntities[i].TakeTurn();
                //UICameraController.Instance.SnapToWorldElement(m_secondTryAIControlledEntities[i].GetWorldTile());
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                m_secondTryAIControlledEntities[i].TakeTurn();
            }
        }

        m_secondTryAIControlledEntities.Clear();

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
