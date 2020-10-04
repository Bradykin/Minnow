using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameMap : GameElementBase
{
    public int m_id;
    public MapDifficulty m_difficulty;

    private List<GameMapEvent> m_mapEvents = new List<GameMapEvent>();
    private List<int> m_mapEventTriggerWaves = new List<int>();

    protected void Init()
    {
        m_icon = UIHelper.GetIconMap(m_name);
    }

    protected void AddMapEvent(GameMapEvent toAdd, int triggerWave)
    {
        m_mapEvents.Add(toAdd);
        m_mapEventTriggerWaves.Add(triggerWave);
    }

    public void TriggerStartIntermissionForWave(int waveNum)
    {
        for (int i = 0; i < m_mapEvents.Count; i++)
        {
            if (m_mapEventTriggerWaves[i] == waveNum)
            {
                m_mapEvents[i].TriggerEvent();
            }
        }

        for (int i = 0; i < m_mapEvents.Count; i++)
        {
            if (m_mapEventTriggerWaves[i] == waveNum-1)
            {
                m_mapEvents[i].EndEvent();
            }
        }
    }
}
