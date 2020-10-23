using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDemonicFireEvent : GameEvent
{
    public ContentDemonicFireEvent(GameTile tile)
    {
        m_name = "Demonic Fire";
        m_eventDesc = "A ghostly fire flies before you. Will you consume it, or let it fly?";
        m_tile = tile;
        m_rarity = GameRarity.Uncommon;

        m_optionOne = new GameEventConsumeFireOption(m_tile, 3);
        m_optionTwo = new GameEventFirestormOption(2, 4);
        m_optionThree = new GameEventLeaveOption();

        LateInit();

        m_minWaveToSpawn = 2;
        m_maxWaveToSpawn = 5;
    }
}

public class GameEventFirestormOption : GameEventOption
{
    private int m_damage;
    private int m_numTimes;

    public GameEventFirestormOption(int damage, int numTimes)
    {
        m_damage = damage;
        m_numTimes = numTimes;
    }

    public override void Init()
    {
        m_message = "Hit <b>all</b> Units for " + m_damage + ", " + m_numTimes + " times.";
    }

    public override void AcceptOption()
    {
        for (int i = 0; i < GameHelper.GetPlayer().m_controlledUnits.Count; i++)
        {
            for (int c = 0; c < m_numTimes; c++)
            {
                GameHelper.GetPlayer().m_controlledUnits[i].GetHit(m_damage);
            }
        }

        for (int i = 0; i < WorldController.Instance.m_gameController.m_gameOpponent.m_controlledUnits.Count; i++)
        {
            for (int c = 0; c < m_numTimes; c++)
            {
                WorldController.Instance.m_gameController.m_gameOpponent.m_controlledUnits[i].GetHit(m_damage);
            }
        }

        EndEvent();
    }
}

public class GameEventConsumeFireOption : GameEventOption
{
    private GameTile m_tile;
    private int m_toGrow;

    public GameEventConsumeFireOption(GameTile tile, int toGrow)
    {
        m_tile = tile;
        m_toGrow = toGrow;
    }

    public override void Init()
    {
        m_message = m_tile.m_occupyingUnit.m_name + " gains '<b>Enrage</b>: Gain +" + m_toGrow + "/+0'";
    }

    public override void AcceptOption()
    {
        m_tile.m_occupyingUnit.AddKeyword(new GameEnrageKeyword(new GameGainStatsAction(m_tile.m_occupyingUnit, m_toGrow, 0)));

        EndEvent();
    }
}