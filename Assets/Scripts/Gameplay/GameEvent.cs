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

    protected virtual void LateInit()
    {
        m_APCost = 2;
    }

    public virtual bool isValidToSpawn(GameTile tile)
    {
        return !tile.HasBuilding();
    }

    //============================================================================================================//

    public string SaveToJson()
    {
        JsonGameEventData jsonData = new JsonGameEventData
        {
            name = m_name
        };

        var export = JsonUtility.ToJson(jsonData);

        return export;
    }

    public void LoadFromJson(JsonGameEventData jsonData)
    {
        //Currently don't need to do anything here
    }
}
