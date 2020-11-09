using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMetaprogressionDataElement
{
    private int m_mapId;
    private int m_chaosNum;
    private int m_level;

    private GameRelic m_relic;
    private GameCard m_card;
    private int m_bonusExp;

    public GameMetaprogressionDataElement(int mapId, int chaosNum, GameRelic relic, int level)
    {
        m_relic = relic;
        m_level = level;

        InitImpl(mapId, chaosNum);
    }

    public GameMetaprogressionDataElement(int mapId, int chaosNum, GameCard card, int level)
    {
        m_card = card;
        m_level = level;

        InitImpl(mapId, chaosNum);
    }

    public GameMetaprogressionDataElement(int mapId, int chaosNum, int exp)
    {
        m_bonusExp = exp;

        InitImpl(mapId, chaosNum);
    }

    private void InitImpl(int mapId, int chaosNum)
    {
        m_mapId = mapId;
        m_chaosNum = chaosNum;
    }

    public Sprite GetIcon()
    {
        if (m_card != null)
        {
            return UIHelper.GetIconCardReward(m_card.GetName(), m_level);
        }
        else if (m_relic != null)
        {
            return UIHelper.GetIconRelicReward(m_relic.GetName(), m_level);
        }
        else if (m_bonusExp > 0)
        {
            return UIHelper.GetIconExpReward(m_bonusExp);
        }

        return null;
    }

    public int GetMapId()
    {
        return m_mapId;
    }

    public int GetChaosLevel()
    {
        return m_chaosNum;
    }

    public GameCard GetCard()
    {
        return m_card;
    }

    public int GetLevel()
    {
        return m_level;
    }

    public int GetBonusExp()
    {
        return m_bonusExp;
    }
}
