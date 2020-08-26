using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGameEnemyEntity
{
    private List<AIStep> m_AISteps;
    public GameEnemyEntity m_gameEnemyEntity { get; private set; }

    public List<GameEntity> m_possibleEntityTargets = new List<GameEntity>();
    public List<GameBuildingBase> m_possibleBuildingTargets = new List<GameBuildingBase>();

    public AIGameEnemyEntity(GameEnemyEntity gameEnemyEntity)
    {
        m_gameEnemyEntity = gameEnemyEntity;
        m_AISteps = new List<AIStep>()
    }

    public void AddAIStep(AIStep AIStep)
    {
        AIStep.m_AIGameEnemyEntity = this;
        m_AISteps.Add(AIStep);
    }

    public void TakeTurn()
    {
        for(int i = 0; i < m_AISteps.Count; i++)
        {
            m_AISteps[i].TakeStep();
        }
    }
}
