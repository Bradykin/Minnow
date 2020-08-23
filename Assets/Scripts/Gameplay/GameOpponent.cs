using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOpponent : ITurns
{
    public List<GameEntity> m_controlledEntities { get; private set; }

    //============================================================================================================//

    private void TakeEntityTurns()
    {
        for (int i = 0; i < m_controlledEntities.Count; i++)
        {

        }
    }

    public void StartTurn()
    {
        Debug.Log("Start opponent turn");
        for (int i = 0; i < m_controlledEntities.Count; i++)
        {
            m_controlledEntities[i].StartTurn();
        }
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
