using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGameEnemyEntity
{
    private List<AIStep> m_AISteps;

    public GameEnemyEntity m_gameEnemyEntity { get; private set; }

    public List<GameEntity> m_possibleEntityTargets = new List<GameEntity>();
    public List<GameBuildingBase> m_possibleBuildingTargets = new List<GameBuildingBase>();

    public List<GameEntity> m_vulnerableEntityTargets = new List<GameEntity>();
    public List<GameBuildingBase> m_vulnerableBuildingTargets = new List<GameBuildingBase>();

    public GameElementBase m_targetGameElement = null;
    public GameTile m_targetGameTile = null;

    public bool m_doSteps = true;

    public AIGameEnemyEntity(GameEnemyEntity gameEnemyEntity)
    {
        m_gameEnemyEntity = gameEnemyEntity;
        m_AISteps = new List<AIStep>();
    }

    public void AddAIStep(AIStep AIStep)
    {
        AIStep.m_AIGameEnemyEntity = this;
        m_AISteps.Add(AIStep);
    }

    public void TakeTurn()
    {
        while (m_doSteps)
        {
            m_doSteps = false;
            for (int i = 0; i < m_AISteps.Count; i++)
            {
                if (!Globals.m_levelActive)
                {
                    break;
                }
                m_AISteps[i].TakeStep();
            }
        }

        m_possibleEntityTargets.Clear();
        m_possibleBuildingTargets.Clear();
        m_vulnerableEntityTargets.Clear();
        m_vulnerableBuildingTargets.Clear();
        m_targetGameElement = null;
        m_doSteps = true;
    }
}
