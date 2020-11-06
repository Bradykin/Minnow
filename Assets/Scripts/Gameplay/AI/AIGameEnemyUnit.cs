using Game.Util;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGameEnemyUnit : ITakeTurnInCoroutineAI
{
    private List<AIStep> m_setupAISteps;
    private List<AIStep> m_activeAISteps;
    
    //private List<AIStep> m_AISteps;

    public GameEnemyUnit m_gameEnemyUnit { get; private set; }

    public List<GameUnit> m_possibleUnitTargets = new List<GameUnit>();
    public List<GameBuildingBase> m_possibleBuildingTargets = new List<GameBuildingBase>();

    public List<GameUnit> m_vulnerableUnitTargets = new List<GameUnit>();
    public List<GameBuildingBase> m_vulnerableBuildingTargets = new List<GameBuildingBase>();

    public List<GameUnit> m_tauntUnitTargets = new List<GameUnit>();

    public GameElementBase m_targetGameElement = null;
    public GameTile m_targetGameTile = null;

    public bool m_doSteps = true;

    public List<string> m_AIDebugLogs = new List<string>();

    public AIDebugTurnLog m_newAIDebugLog = null;

    public bool UseSteppedOutTurn =>
        PlayerDataManager.PlayerAccountData.m_followEnemy &&
        (!m_gameEnemyUnit.GetGameTile().m_isFog
        || (m_targetGameTile != null && !m_targetGameTile.m_isFog)
        || (m_targetGameElement != null && m_targetGameElement is GameUnit gameUnitBase && !gameUnitBase.GetGameTile().m_isFog)
        || (m_targetGameElement != null && m_targetGameElement is GameBuildingBase gameBuildingBase && !gameBuildingBase.GetGameTile().m_isFog));

    public AIGameEnemyUnit(GameEnemyUnit gameEnemyUnit)
    {
        m_gameEnemyUnit = gameEnemyUnit;
        m_setupAISteps = new List<AIStep>();
        m_activeAISteps = new List<AIStep>();
    }

    public void AddAIStep(AIStep AIStep, bool isSetup)
    {
        AIStep.m_AIGameEnemyUnit = this;
        if (isSetup)
        {
            m_setupAISteps.Add(AIStep);
        }
        else
        {
            m_activeAISteps.Add(AIStep);
        }
    }

    //============================================================================================================//

    public void SetupTurn()
    {
        m_newAIDebugLog = new AIDebugTurnLog();

        for (int i = 0; i < m_setupAISteps.Count; i++)
        {
            if (GameHelper.GetGameController().m_runStateType == RunStateType.None)
            {
                break;
            }

            FactoryManager.Instance.StartCoroutine(m_setupAISteps[i].TakeStep(false));
        }
    }

    public IEnumerator TakeTurn(bool shouldYield)
    {
        while (!m_gameEnemyUnit.m_isDead && m_doSteps)
        {
            m_doSteps = false;
            for (int i = 0; i < m_activeAISteps.Count; i++)
            {
                if (GameHelper.GetGameController().m_runStateType == RunStateType.None)
                {
                    break;
                }

                if (shouldYield)
                {
                    yield return FactoryManager.Instance.StartCoroutine(m_activeAISteps[i].TakeStep(shouldYield));
                }
                else
                {
                    FactoryManager.Instance.StartCoroutine(m_activeAISteps[i].TakeStep(shouldYield));
                }
            }

            if (m_doSteps)
            {
                CleanupTurn();
                SetupTurn();
            }
        }

        CleanupTurn();
    }

    public void CleanupTurn()
    {
        m_newAIDebugLog.m_waveNumber = GameHelper.GetCurrentWaveNum();
        m_newAIDebugLog.m_turnNumber = GameHelper.GetGameController().m_currentTurnNumber;
        if (m_targetGameElement == null)
        {
            m_newAIDebugLog.m_targetGameElementName = "Null";
        }
        else
        {
            m_newAIDebugLog.m_targetGameElementName = m_targetGameElement.GetName();
        }

        if (m_targetGameTile == null)
        {
            m_newAIDebugLog.m_targetGameTileLocation = "Null";
        }
        else
        {
            m_newAIDebugLog.m_targetGameTileLocation = m_targetGameTile.m_gridPosition.ToString();
        }

        for (int i = 0; i < m_possibleUnitTargets.Count; i++)
        {
            m_newAIDebugLog.m_possibleUnitTargets.Add(m_possibleUnitTargets[i].GetName());
        }

        for (int i = 0; i < m_possibleBuildingTargets.Count; i++)
        {
            m_newAIDebugLog.m_possibleBuildingTargets.Add(m_possibleBuildingTargets[i].GetName());
        }

        for (int i = 0; i < m_vulnerableUnitTargets.Count; i++)
        {
            m_newAIDebugLog.m_vulnerableUnitTargets.Add(m_vulnerableUnitTargets[i].GetName());
        }

        for (int i = 0; i < m_vulnerableBuildingTargets.Count; i++)
        {
            m_newAIDebugLog.m_vulnerableBuildingTargets.Add(m_vulnerableBuildingTargets[i].GetName());
        }

        m_AIDebugLogs.Add(JsonConvert.SerializeObject(m_newAIDebugLog));
        m_newAIDebugLog = null;

        m_possibleUnitTargets.Clear();
        m_possibleBuildingTargets.Clear();
        m_vulnerableUnitTargets.Clear();
        m_vulnerableBuildingTargets.Clear();
        m_tauntUnitTargets.Clear();
        m_targetGameElement = null;
        m_targetGameTile = null;
        m_doSteps = true;
    }
}
