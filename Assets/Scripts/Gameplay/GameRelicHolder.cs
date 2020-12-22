using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRelicHolder : ISave<JsonGameRelicHolderData>, ILoad<JsonGameRelicHolderData>
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

    public GameRelic GetRelic<T>()
    {
        for (int i = 0; i < m_relics.Count; i++)
        {
            if (m_relics[i] is T val)
            {
                return m_relics[i];
            }
        }

        return null;
    }

    public int GetSize()
    {
        return m_relics.Count;
    }

    public List<GameRelic> GetRelicListForRead()
    {
        return m_relics;
    }

    public JsonGameRelicHolderData SaveToJson()
    {
        JsonGameRelicHolderData jsonData = new JsonGameRelicHolderData
        {
            jsonGameRelicDatas = new List<JsonGameRelicData>()
        };

        foreach (GameRelic relic in m_relics)
        {
            jsonData.jsonGameRelicDatas.Add(relic.SaveToJson());
        }

        return jsonData;
    }

    public void LoadFromJson(JsonGameRelicHolderData jsonData)
    {
        foreach (JsonGameRelicData jsonGameRelic in jsonData.jsonGameRelicDatas)
        {
            AddRelic(GameRelicFactory.GetRelicFromJson(jsonGameRelic));
        }
    }
}
