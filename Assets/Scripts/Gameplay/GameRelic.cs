using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameRelic : GameElementBase, ISave<JsonGameRelicData>, ILoad<JsonGameRelicData>
{
    public int m_storedTagWeight;

    protected void LateInit()
    {
        m_icon = UIHelper.GetIconRelic(m_name);
    }

    public virtual string GetDesc()
    {
        return m_desc;
    }

    public JsonGameRelicData SaveToJson()
    {
        JsonGameRelicData jsonData = new JsonGameRelicData
        {
            name = GetName()
        };

        return jsonData;
    }

    public void LoadFromJson(JsonGameRelicData jsonData)
    {
        //Currently don't need to do anything here
    }
}
