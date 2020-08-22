using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRelicHolder
{
    public List<GameRelic> m_relics;

    public GameRelicHolder()
    {
        m_relics = new List<GameRelic>();
    }

    public T GetRelic<T>()
    {
        for (int i = 0; i < m_relics.Count; i++)
        {
            if (m_relics[i] is T val)
            {
                return val;
            }
        }

        return default(T);
    }
}
