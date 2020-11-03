using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentForestOrCityEvent : GameEvent
{
    public ContentForestOrCityEvent(GameTile tile)
    {
        m_name = "Forest Or City";
        m_eventDesc = "While sleeping at camp, your troops hear a whisper forcing them to choose between the wisdom of the forest and the knowledge of the city.";
        m_tile = tile;

        if (m_tile == null)
        {
            return;
        }

        m_optionOne = new GameEventGiveKeywordOption(m_tile, new GameForestwalkKeyword());
        m_optionTwo = new GameEventGiveKeywordOption(m_tile, new GameKnowledgeableKeyword(new GameGainStaminaAction(m_tile.m_occupyingUnit, 1)));

        LateInit();
    }
}