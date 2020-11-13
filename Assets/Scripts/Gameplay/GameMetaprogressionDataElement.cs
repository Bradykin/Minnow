using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMetaprogressionDataElement
{
    private int m_mapId;

    private GameRelic m_relic;
    private GameCard m_card;
    private int m_bonusExp;

    public GameMetaprogressionDataElement(int mapId, GameRelic relic)
    {
        m_relic = relic;

        m_mapId = mapId;
    }

    public GameMetaprogressionDataElement(int mapId, GameCard card)
    {
        m_card = card;

        m_mapId = mapId;
    }

    public GameMetaprogressionDataElement(int mapId, int exp)
    {
        m_bonusExp = exp;

        m_mapId = mapId;
    }

    public Sprite GetIcon()
    {
        if (m_card != null)
        {
            //return UIHelper.GetIconCardReward(m_card.GetName()); nmartino
        }
        else if (m_relic != null)
        {
            //return UIHelper.GetIconRelicReward(m_relic.GetName());
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

    public GameCard GetCard()
    {
        return m_card;
    }

    public GameRelic GetRelic()
    {
        return m_relic;
    }

    public int GetBonusExp()
    {
        return m_bonusExp;
    }
}
