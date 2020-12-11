using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReturnToDeckAction : GameAction
{
    private GameUnit m_retuningUnit;

    public GameReturnToDeckAction(GameUnit returningUnit)
    {
        m_retuningUnit = returningUnit;

        m_name = "Return to Deck";
        m_actionParamType = ActionParamType.UnitParam;
    }

    public override string GetDesc()
    {
        return "Return " + m_retuningUnit.GetName() + " to your deck.";
    }

    public override void DoAction()
    {
        if (m_retuningUnit.m_returnedToDeckDeath)
        {
            return;
        }
        m_retuningUnit.m_returnedToDeckDeath = true;

        GameUnitCard cardFromUnit = GameCardFactory.GetCardFromUnit(m_retuningUnit);
        GameHelper.GetPlayer().m_curDeck.AddToDiscard(cardFromUnit);
    }

    public override void AddAction(GameAction toAdd)
    {
        //Stacking this action does nothing.
    }

    public override void SubtractAction(GameAction toSubtract)
    {
        //Subtracting this action does nothing.
    }

    public override bool ShouldBeRemoved()
    {
        return false;
    }

    public override GameUnit GetGameUnit()
    {
        return m_retuningUnit;
    }

    public override JsonGameActionData SaveToJson()
    {
        JsonGameActionData jsonData = new JsonGameActionData
        {
            name = m_name
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonGameActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}