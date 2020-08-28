using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMillitiaEvent : GameEvent
{
    public ContentMillitiaEvent(GameTile tile)
    {
        m_name = "Millitia";
        m_eventDesc = "A band of millitia is fighting off some enemies here.  Looks like you could help!";
        m_tile = tile;
        m_rarity = GameRarity.Common;

        m_optionOne = new GameEventHelpMillitiaOption(m_tile);
        m_optionTwo = new GameEventLeaveOption();

        LateInit();
    }
}

public class GameEventHelpMillitiaOption : GameEventOption
{
    private GameTile m_tile;

    public GameEventHelpMillitiaOption(GameTile tile)
    {
        m_tile = tile;

        m_hasTooltip = true;
    }

    public override string GetMessage()
    {
        GameCardEntityBase toGainCard = new ContentDwarvenSoldierCard();

        m_message = "Sacrifice " + m_tile.m_occupyingEntity.m_name + ", but gain 5 " + toGainCard.m_name + " cards.";

        return base.GetMessage();
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        m_tile.m_occupyingEntity.Die();

        for (int i = 0; i < 5; i++)
        {
            player.AddCardToDeck(new ContentDwarvenSoldierCard());
        }

        EndEvent();
    }

    public override void BuildTooltip()
    {
        GameCardEntityBase toGainCard = new ContentDwarvenSoldierCard();

        if (m_tile.m_occupyingEntity.m_name != toGainCard.GetEntity().m_name)
        {
            UIHelper.CreateEntityTooltip(m_tile.m_occupyingEntity);
        }
        UIHelper.CreateEntityTooltip(toGainCard.GetEntity());
    }
}
