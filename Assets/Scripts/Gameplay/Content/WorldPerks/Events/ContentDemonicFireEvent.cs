using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDemonicFireEvent : GameEvent
{
    public ContentDemonicFireEvent(GameTile tile)
    {
        m_name = "Demonic Fire";
        m_eventDesc = "Reports of a mysterious ghostly fire flying around this region flood in.";
        m_tile = tile;

        Init();
    }

    public override void LateInit()
    {
        m_optionOne = new GameEventConsumeFireOption(m_tile, 3);
        m_optionTwo = new GameEventFirestormOption(2, 4);

        base.LateInit();
    }

    public override string GetOptionOneTooltip()
    {
        return "Give the unit that goes here '<b>Enrage</b>: +3/+0'.";
    }

    public override string GetOptionTwoTooltip()
    {
        return "Hit <b>all</b> units on the map for 2 damage 4 times.";
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
        List<GameUnit> playerUnits = GameHelper.GetPlayer().m_controlledUnits;
        for (int i = playerUnits.Count-1; i >= 0; i--)
        {
            for (int c = 0; c < m_numTimes; c++)
            {
                if (!playerUnits[i].m_isDead)
                {
                    playerUnits[i].GetHitByAbility(m_damage);
                }
            }
        }

        List<GameEnemyUnit> enemyUnits = WorldController.Instance.m_gameController.m_gameOpponent.m_controlledUnits;
        for (int i = enemyUnits.Count-1; i >= 0; i--)
        {
            for (int c = 0; c < m_numTimes; c++)
            {
                if (!enemyUnits[i].m_isDead)
                {
                    enemyUnits[i].GetHitByAbility(m_damage);
                }
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
        m_message = m_tile.GetOccupyingUnit().GetName() + " gains '<b>Enrage</b>: Gain +" + m_toGrow + "/+0'";
    }

    public override void AcceptOption()
    {
        m_tile.GetOccupyingUnit().AddKeyword(new GameEnrageKeyword(new GameGainStatsAction(m_tile.GetOccupyingUnit(), m_toGrow, 0)));

        EndEvent();
    }
}