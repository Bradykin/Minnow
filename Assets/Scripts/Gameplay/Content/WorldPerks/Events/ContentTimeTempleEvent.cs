using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTimeTempleEvent : GameEvent
{
    public ContentTimeTempleEvent(GameTile tile)
    {
        m_name = "Time Temple";
        m_eventDesc = "At the midnight hour, a strange temple is said to appear here.";
        m_tile = tile;

        Init();
    }

    public override void LateInit()
    {
        m_optionOne = new GameEventSpeedTimeOption();
        m_optionTwo = new GameEventSlowTimeOption();

        base.LateInit();
    }

    public override string GetOptionOneTooltip()
    {
        return "Move the wave timer forward by 1 turn.";
    }

    public override string GetOptionTwoTooltip()
    {
        return "Move the wave timer backwards by 1 turn.";
    }
}

public class GameEventSpeedTimeOption : GameEventOption
{
    private int m_timeIncrease = 1;

    public override string GetMessage()
    {
        m_message = "Speed up time, jumping " + m_timeIncrease + " rounds ahead.";

        return base.GetMessage();
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        //WorldController.Instance.m_gameController.m_currentTurnNumber += m_timeIncrease;
        WorldController.Instance.m_gameController.CheckStartIntermission();

        EndEvent();
    }
}

public class GameEventSlowTimeOption : GameEventOption
{
    private int m_timeDecrease = 1;

    public override string GetMessage()
    {
        m_message = "Slow down time, pulling back " + m_timeDecrease + " rounds.";

        return base.GetMessage();
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        /*WorldController.Instance.m_gameController.m_currentTurnNumber -= m_timeDecrease;
        if (WorldController.Instance.m_gameController.m_currentTurnNumber < 0)
        {
            WorldController.Instance.m_gameController.m_currentTurnNumber = 0;
        }
        WorldController.Instance.m_gameController.CheckStartIntermission();*/

        EndEvent();
    }
}
