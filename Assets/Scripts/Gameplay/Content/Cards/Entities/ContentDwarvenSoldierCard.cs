using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDwarvenSoldierCard : GameUnitCard
{
    private int m_mapUnlockID = 0;
    private int m_rankOneChaosLevel = 4;
    private int m_rankTwoChaosLevel = 7;
    private int m_rankThreeChaosLevel = 10;

    public ContentDwarvenSoldierCard()
    {
        SetCardLevel(GetCardLevel());

        m_unit = new ContentDwarvenSoldier();

        m_unit.SetUnitLevel(m_cardLevel);

        m_cost = 1;

        FillBasicData();
    }

    public int GetCardLevel()
    {
        if (!GameMetaProgression.IsMapUnlocked(m_mapUnlockID))
        {
            return 0;
        }

        if (GameMetaProgression.IsChaosLevelAchieved(m_mapUnlockID, m_rankThreeChaosLevel))
        {
            return 3;
        }

        if (GameMetaProgression.IsChaosLevelAchieved(m_mapUnlockID, m_rankTwoChaosLevel))
        {
            return 2;
        }

        if (GameMetaProgression.IsChaosLevelAchieved(m_mapUnlockID, m_rankOneChaosLevel))
        {
            return 1;
        }

        return 0;
    }
}
