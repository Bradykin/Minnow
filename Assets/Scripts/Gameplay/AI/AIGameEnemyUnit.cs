using Game.Util;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGameEnemyUnit
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
    public bool m_exitSteps = false;

    public List<string> m_AIDebugLogs = new List<string>();

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
        if (GameHelper.GetGameController() == null)
        {
            return;
        }

        for (int i = 0; i < m_setupAISteps.Count; i++)
        {
            if (GameHelper.GetGameController().m_runStateType == RunStateType.None)
            {
                break;
            }

            m_setupAISteps[i].TakeStepInstant();
        }
    }

    public IEnumerator TakeTurnCoroutine()
    {
        while (!m_gameEnemyUnit.m_isDead && m_doSteps)
        {
            m_doSteps = false;
            for (int i = 0; i < m_activeAISteps.Count; i++)
            {
                if (!WorldController.Instance.m_isInGame)
                {
                    break;
                }
                
                if (GameHelper.GetGameController().m_runStateType == RunStateType.None)
                {
                    break;
                }

                 yield return FactoryManager.Instance.StartCoroutine(m_activeAISteps[i].TakeStepCoroutine());
            }

            if (m_doSteps)
            {
                CleanupTurn();
                SetupTurn();
            }
        }

        CleanupTurn();
    }

    public void TakeTurnInstant()
    {
        while (!m_gameEnemyUnit.m_isDead && m_doSteps)
        {
            m_doSteps = false;
            for (int i = 0; i < m_activeAISteps.Count; i++)
            {
                if (!WorldController.Instance.m_isInGame)
                {
                    break;
                }

                if (GameHelper.GetGameController().m_runStateType == RunStateType.None)
                {
                    break;
                }

                m_activeAISteps[i].TakeStepInstant();

                if (m_exitSteps)
                {
                    break;
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
        if (GameHelper.GetGameController() == null)
        {
            return;
        }

        m_possibleUnitTargets.Clear();
        m_possibleBuildingTargets.Clear();
        m_vulnerableUnitTargets.Clear();
        m_vulnerableBuildingTargets.Clear();
        m_tauntUnitTargets.Clear();
        m_targetGameElement = null;
        m_targetGameTile = null;
        m_doSteps = true;
        m_exitSteps = false;
    }
}
