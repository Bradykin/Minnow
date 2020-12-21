using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGoldenFruitEvent : GameEvent
{
    public ContentGoldenFruitEvent(GameTile tile)
    {
        m_name = "Golden Fruit";
        m_eventDesc = "Legends tell of a golden fruit in this region. It looks delicious, but just carrying it is said to have magical properties.";
        m_tile = tile;

        Init();
    }

    public override void LateInit()
    {
        m_optionOne = new GameEventGiveKeywordOption(m_tile, new GameRegenerateKeyword(20));
        m_optionTwo = new GameEventStatsBuffOption(m_tile, 0, 40);

        base.LateInit();
    }

    public override string GetOptionOneTooltip()
    {
        return "Give the unit that goes here '<b>Regen</b> 20 <b>permanently</b>.";
    }

    public override string GetOptionTwoTooltip()
    {
        return "Give the unit that goes here +0/+40 <b>permanently</b>.";
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
        m_message = m_tile.GetOccupyingUnit().GetName() + " gains " + m_keyword.GetDisplayString() + ".";

        return base.GetMessage();
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        m_tile.GetOccupyingUnit().AddKeyword(m_keyword, true, false);

        EndEvent();
    }

    //Intentionally left blank
    public override void DeclineOption()
    {

    }

    public override void BuildTooltip()
    {
        UIHelper.CreateUnitTooltip(m_tile.GetOccupyingUnit());
    }
}
