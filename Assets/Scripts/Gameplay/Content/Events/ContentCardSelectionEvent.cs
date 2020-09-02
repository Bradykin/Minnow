using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCardSelectionEvent : GameEvent
{
    public ContentCardSelectionEvent()
    {
        m_name = "Select a Card";
        m_eventDesc = "Select one of the following cards.";
        m_rarity = GameRarity.Common;

        m_optionOne = new GameEventCardSelectOption(GameCardFactory.GetRandomStandardEntityCard());
        m_optionTwo = new GameEventCardSelectOption(GameCardFactory.GetRandomStandardSpellCard());
        m_optionThree = new GameEventCardSelectOption(GameCardFactory.GetRandomStandardCard());

        LateInit();
    }
}

public class GameEventCardSelectOption : GameEventOption
{
    private GameCard m_card;

    public GameEventCardSelectOption(GameCard card)
    {
        m_card = card;

        m_hasTooltip = true;
    }

    public override string GetMessage()
    {
        GameCardEntityBase toGainCard = new ContentDwarvenSoldierCard();

        m_message = "Gain " + m_card.m_name + ".";

        return base.GetMessage();
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        player.AddCardToDeck(new ContentDwarvenSoldierCard());

        EndEvent();
    }

    public override void BuildTooltip()
    {
        if (m_card is GameCardEntityBase)
        {
            GameEntity entity = ((GameCardEntityBase)m_card).GetEntity();

            UIHelper.CreateEntityTooltip(entity, false);
        }
        else
        {
            UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip(m_card.m_name, m_card.m_desc));
        }
    }
}
