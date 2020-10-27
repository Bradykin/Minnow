using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameRelic : GameElementBase
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

    public virtual int GetRelicLevel()
    {
        //TODO: alex - Hook this up to player save data.

        return 0;
    }
}
