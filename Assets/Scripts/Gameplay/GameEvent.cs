using Game.Util;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent : GameElementBase, ISave<JsonGameEventData>, ILoad<JsonGameEventData>
{
    public GameTile m_tile;
    public int m_staminaCost;
    public string m_eventDesc;

    public bool m_isComplete;

    public GameEventOption m_optionOne;
    public GameEventOption m_optionTwo;
    public GameEventOption m_optionThree;

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
        m_staminaCost = 2;
    }

    //============================================================================================================//

    public JsonGameEventData SaveToJson()
    {
        JsonGameEventData jsonData = new JsonGameEventData
        {
            name = m_name
        };

        return jsonData;
    }

    public void LoadFromJson(JsonGameEventData jsonData)
    {
        //Currently don't need to do anything here
    }
}
