using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGoldenFruitEvent : GameEvent
{
    public ContentGoldenFruitEvent(GameTile tile)
    {
        m_name = "Golden Fruit";
        m_eventDesc = "A golden fruit falls on the head of one of your troops.  It looks delicious, but just carrying it seems to have magical properties.";
        m_tile = tile;

        if (m_tile == null)
        {
            return;
        }

        m_optionOne = new GameEventGiveKeywordOption(m_tile, new GameMomentumKeyword(new GameHealAction(m_tile.m_occupyingUnit, 3)));
        m_optionTwo = new GameEventStatsBuffOption(m_tile, 0, 20);
        m_optionThree = new GameEventLeaveOption();

        LateInit();
    }
}

public class GameEventGiveKeywordOption : GameEventOption
{
    private GameTile m_tile;
    private GameKeywordBase m_keyword;

    public GameEventGiveKeywordOption(GameTile tile, GameKeywordBase keyword)
    {
        m_keyword = keyword;

        m_tile = tile;

        m_hasTooltip = true;
    }

    public override string GetMessage()
    {
        m_message = m_tile.m_occupyingUnit.GetName() + " gains " + m_keyword.GetDisplayString() + ".";

        return base.GetMessage();
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        m_tile.m_occupyingUnit.AddKeyword(m_keyword);

        EndEvent();
    }

    public override void BuildTooltip()
    {
        UIHelper.CreateUnitTooltip(m_tile.m_occupyingUnit);
    }
}
