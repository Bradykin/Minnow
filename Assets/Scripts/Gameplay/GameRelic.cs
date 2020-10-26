using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameRelic : GameElementBase
{
    public int m_storedTagWeight;

    protected int m_playerUnlockLevel;
    protected int m_relicLevel;

    //Starter Card Data
    protected int m_mapUnlockID = 0;

    protected void LateInit()
    {
        m_icon = UIHelper.GetIconRelic(m_name);
    }

    public virtual string GetDesc()
    {
        return m_desc;
    }

    public int GetPlayerUnlockLevel()
    {
        return m_playerUnlockLevel;
    }

    public void SetRelicLevel(int level)
    {
        m_relicLevel = level;
    }

    public virtual int GetRelicLevel()
    {
        if (!PlayerDataManager.IsMapUnlocked(m_mapUnlockID))
        {
            return 0;
        }

        if (PlayerDataManager.IsChaosLevelAchieved(m_mapUnlockID, Constants.RankOneChaosLevel))
        {
            return 2;
        }

        if (PlayerDataManager.IsChaosLevelAchieved(m_mapUnlockID, Constants.RankTwoChaosLevel))
        {
            return 1;
        }

        return 0;
    }
}
