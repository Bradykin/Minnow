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
    private GameCard m_card;

    public GameEventHelpMillitiaOption(GameTile tile)
    {
        m_tile = tile;
        m_card = new ContentDwarvenSoldierCard();

        m_hasTooltip = true;
    }

    public override string GetMessage()
    {
        m_message = "Sacrifice " + m_tile.m_occupyingEntity.m_name + ", but gain 2 " + m_card.m_name + " cards.";

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

        for (int i = 0; i < 2; i++)
        {
            player.AddCardToDiscard(GameCardFactory.GetCardClone(m_card), true);
        }

        EndEvent();
    }

    public override void BuildTooltip()
    {
        if (m_tile.m_occupyingEntity == null)
        {
            return;
        }

        GameCardEntityBase toGainCard = (GameCardEntityBase)m_card;

        if (m_tile.m_occupyingEntity.m_name != toGainCard.GetEntity().m_name)
        {
            UIHelper.CreateEntityTooltip(m_tile.m_occupyingEntity);
        }
        UIHelper.CreateEntityTooltip(toGainCard.GetEntity());
    }
}
