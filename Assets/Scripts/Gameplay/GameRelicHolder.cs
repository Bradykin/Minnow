using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRelicHolder
{
    private List<GameRelic> m_relics;

    public GameRelicHolder()
    {
        m_relics = new List<GameRelic>();
    }

    public void AddRelic(GameRelic relic)
    {
        m_relics.Add(relic);
        UIRelicController.Instance.AddRelic(relic);
    }

    public int GetNumRelics<T>()
    {
        int count = 0;
        for (int i = 0; i < m_relics.Count; i++)
        {
            if (m_relics[i] is T val)
            {
                count++;
            }
        }

        return count;
    }

    public List<GameRelic> GetRelicListForRead()
    {
        return m_relics;
    }
}
