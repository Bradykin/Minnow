using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMillitiaEvent : GameEvent
{
    public ContentMillitiaEvent(GameTile tile)
    {
        m_name = "Millitia";
        m_eventDesc = "A band of millitia is fighting off some enemies here.";
        m_tile = tile;

        Init();
    }

    public override void LateInit()
    {
        m_optionOne = new GameEventHelpMillitiaOption(m_tile);

        base.LateInit();
    }

    public override string GetOptionOneTooltip()
    {
        return "Sacrifice the unit that goes here for the wave, but gain a random rare unit permanently.";
    }

    public override string GetOptionTwoTooltip()
    {
        return "";
    }
}

public class GameEventHelpMillitiaOption : GameEventOption
{
    private GameTile m_tile;
    private GameCard m_card;

    public GameEventHelpMillitiaOption(GameTile tile)
    {
        m_tile = tile;
        m_card = GameCardFactory.GetRandomStandardUnitCard(GameElementBase.GameRarity.Rare);

        m_hasTooltip = true;
    }

    public override string GetMessage()
    {
        m_message = "Sacrifice " + m_tile.m_occupyingUnit.GetName() + ", but gain a " + m_card.GetName() + " card.";

        return base.GetMessage();
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        m_tile.m_occupyingUnit.Die();

        player.AddCardToDiscard(GameCardFactory.GetCardClone(m_card), true);

        EndEvent();
    }

    public override void BuildTooltip()
    {
        if (m_tile.m_occupyingUnit == null)
        {
            return;
        }

        GameUnitCard toGainCard = (GameUnitCard)m_card;

        if (m_tile.m_occupyingUnit.GetName() != toGainCard.GetUnit().GetName())
        {
            UIHelper.CreateUnitTooltip(m_tile.m_occupyingUnit, true);
        }
        UIHelper.CreateUnitTooltip(toGainCard.GetUnit());
    }
}
