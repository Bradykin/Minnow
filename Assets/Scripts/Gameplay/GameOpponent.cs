using Game.Util;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class GameOpponent : ITurns
{
    public List<GameEnemyEntity> m_controlledEntities { get; private set; }

    public GameOpponent()
    {
        m_controlledEntities = new List<GameEnemyEntity>();
    }

    public void LateInit()
    {
        Debug.Log("GameOpponent LateInit");
        WorldGridManager.Instance.SetupEnemies(this);
    }

    //============================================================================================================//

    private void TakeEntityTurns()
    {
        for (int i = 0; i < m_controlledEntities.Count; i++)
        {
            m_controlledEntities[i].TakeTurn();
        }
        WorldController.Instance.m_gameController.MoveToNextTurn();
    }

    public void StartTurn()
    {
        Debug.Log("Start opponent turn");
        for (int i = 0; i < m_controlledEntities.Count; i++)
        {
            m_controlledEntities[i].StartTurn();
        }
        TakeEntityTurns();
    }

    public void EndTurn()
    {
        Debug.Log("End opponent turn");
        for (int i = 0; i < m_controlledEntities.Count; i++)
        {
            m_controlledEntities[i].EndTurn();
        }
    }
}
