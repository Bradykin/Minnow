using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent : GameElementBase, ISave, ILoad<JsonGameEventData>
{
    public GameTile m_tile;
    public int m_APCost;
    public string m_eventDesc;

    public bool m_isComplete;

    public GameEventOption m_optionOne;
    public GameEventOption m_optionTwo;
    public GameEventOption m_optionThree;

    protected int m_minWaveToSpawn;
    protected int m_maxWaveToSpawn;

    public void Init()
    {
        if (m_optionOne != null)
        {
            m_optionOne.Init();
        }

        if (m_optionTwo != null)
        {
            m_optionTwo.Init();
        }
        
        if (m_optionThree != null)
        {
            m_optionThree.Init();
        }
    }

    protected virtual void LateInit()
    {
        m_APCost = 2;
    }

    public virtual bool IsValidToSpawn()
    {
        int currentTurn = GameHelper.GetGameController().m_currentWaveTurn;

        if (currentTurn >= m_minWaveToSpawn && currentTurn <= m_maxWaveToSpawn)
        {
            return true;
        }

        return false;
    }

    //============================================================================================================//

    public JsonGameEventData SaveToJsonAsJson()
    {
        JsonGameEventData jsonData = new JsonGameEventData
        {
            name = m_name
        };

        return jsonData;
    }

    public string SaveToJsonAsString()
    {
        JsonGameEventData jsonData = SaveToJsonAsJson();

        var export = JsonUtility.ToJson(jsonData);

        return export;
    }

    public void LoadFromJson(JsonGameEventData jsonData)
    {
        //Currently don't need to do anything here
    }
}
