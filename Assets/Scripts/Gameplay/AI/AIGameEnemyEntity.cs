using Game.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGameEnemyUnit : ITakeTurnAI
{
    private List<AIStep> m_AISteps;

    public GameEnemyUnit m_gameEnemyUnit { get; private set; }

    public List<GameUnit> m_possibleUnitTargets = new List<GameUnit>();
    public List<GameBuildingBase> m_possibleBuildingTargets = new List<GameBuildingBase>();

    public List<GameUnit> m_vulnerableUnitTargets = new List<GameUnit>();
    public List<GameBuildingBase> m_vulnerableBuildingTargets = new List<GameBuildingBase>();

    public GameElementBase m_targetGameElement = null;
    public GameTile m_targetGameTile = null;

    public bool m_doSteps = true;

    public List<string> m_AIDebugLogs = new List<string>();

    public AIDebugTurnLog m_newAIDebugLog = null;

    public bool UseSteppedOutTurn => 
        Constants.UseSteppedOutEnemyTurns &&
        (!m_gameEnemyUnit.GetGameTile().IsInFog()
        || (m_targetGameTile != null && !m_targetGameTile.IsInFog())
        || (m_targetGameElement != null && m_targetGameElement is GameUnit gameUnitBase && !gameUnitBase.GetGameTile().IsInFog())
        || (m_targetGameElement != null && m_targetGameElement is GameBuildingBase gameBuildingBase && !gameBuildingBase.GetGameTile().IsInFog()));

    public AIGameEnemyUnit(GameEnemyUnit gameEnemyUnit)
    {
        m_gameEnemyUnit = gameEnemyUnit;
        m_AISteps = new List<AIStep>();
    }

    public void AddAIStep(AIStep AIStep)
    {
        AIStep.m_AIGameEnemyUnit = this;
        m_AISteps.Add(AIStep);
    }

    public IEnumerator TakeTurn()
    {
        m_newAIDebugLog = new AIDebugTurnLog();

        int indentifier = UnityEngine.Random.Range(0, 1000000);

        while (m_doSteps)
        {
            m_doSteps = false;
            for (int i = 0; i < m_AISteps.Count; i++)
            {
                Debug.Log(m_gameEnemyUnit.m_name + " IDENTIFIER " + indentifier + " DOING " + m_AISteps[i].GetType());
                Debug.Log(DateTime.Now + " -- " + DateTime.Now.Millisecond);
                if (!Globals.m_levelActive)
                {
                    break;
                }
                yield return m_AISteps[i].TakeStep();
            }
        }

        m_newAIDebugLog.m_waveNumber = GameHelper.GetGameController().m_waveNum;
        m_newAIDebugLog.m_turnNumber = GameHelper.GetGameController().m_currentWaveTurn;
        if (m_targetGameElement == null)
        {
            m_newAIDebugLog.m_targetGameElementName = "Null";
        }
        else
        {
            m_newAIDebugLog.m_targetGameElementName = m_targetGameElement.m_name;
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
            m_newAIDebugLog.m_possibleUnitTargets.Add(m_possibleUnitTargets[i].m_name);
        }

        for (int i = 0; i < m_possibleBuildingTargets.Count; i++)
        {
            m_newAIDebugLog.m_possibleBuildingTargets.Add(m_possibleBuildingTargets[i].m_name);
        }

        for (int i = 0; i < m_vulnerableUnitTargets.Count; i++)
        {
            m_newAIDebugLog.m_vulnerableUnitTargets.Add(m_vulnerableUnitTargets[i].m_name);
        }

        for (int i = 0; i < m_vulnerableBuildingTargets.Count; i++)
        {
            m_newAIDebugLog.m_vulnerableBuildingTargets.Add(m_vulnerableBuildingTargets[i].m_name);
        }

        m_AIDebugLogs.Add(JsonUtility.ToJson(m_newAIDebugLog));
        m_newAIDebugLog = null;

        m_possibleUnitTargets.Clear();
        m_possibleBuildingTargets.Clear();
        m_vulnerableUnitTargets.Clear();
        m_vulnerableBuildingTargets.Clear();
        m_targetGameElement = null;
        m_targetGameTile = null;
        m_doSteps = true;
    }
}
