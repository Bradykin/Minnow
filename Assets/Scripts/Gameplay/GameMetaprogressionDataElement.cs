using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMetaprogressionDataElement
{
    private GameMap m_map;

    private GameRelic m_relic;
    private GameCard m_card;
    private int m_bonusExp;

    public GameMetaprogressionDataElement(GameMap map, GameRelic relic)
    {
        m_relic = relic;

        m_map = map;
    }

    public GameMetaprogressionDataElement(GameMap map, GameCard card)
    {
        m_card = card;

        m_map = map;
    }

    public GameMetaprogressionDataElement(GameMap map, int exp)
    {
        m_bonusExp = exp;

        m_map = map;
    }

    public GameMap GetMap()
    {
        return m_map;
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
