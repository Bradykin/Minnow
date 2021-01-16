using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReturnToDeckBuffedAction : GameAction
{
    private GameUnit m_retuningUnit;
    private int m_attackBuff;
    private int m_healthBuff;

    public GameReturnToDeckBuffedAction(GameUnit returningUnit, int attackBuff, int healthBuff)
    {
        m_retuningUnit = returningUnit;
        m_attackBuff = attackBuff;
        m_healthBuff = healthBuff;

        m_name = "Return to Deck Buffed";
        m_actionParamType = ActionParamType.UnitTwoIntParam;
    }

    public override string GetDesc()
    {
        return "Return " + m_retuningUnit.GetName() + " to your discard, also giving it +" + m_attackBuff + "/+" + m_healthBuff + ".";
    }

    public override void DoAction()
    {
        if (m_retuningUnit.m_returnedToDeckDeath)
        {
            return;
        }

        AudioHelper.PlaySFX(AudioHelper.UICardClick);

        m_retuningUnit.m_returnedToDeckDeath = true;

        m_retuningUnit.AddStats(m_attackBuff, m_healthBuff, false, false);

        GameUnitCard cardFromUnit = GameCardFactory.GetCardFromUnit(m_retuningUnit);
        GameHelper.GetPlayer().m_curDeck.AddToDiscard(cardFromUnit);
    }

    public override void AddAction(GameAction toAdd)
    {
        GameReturnToDeckBuffedAction tempAction = (GameReturnToDeckBuffedAction)toAdd;

        m_attackBuff += tempAction.m_attackBuff;
        m_healthBuff += tempAction.m_healthBuff;
    }

    public override void SubtractAction(GameAction toSubtract)
    {
        GameReturnToDeckBuffedAction tempAction = (GameReturnToDeckBuffedAction)toSubtract;

        m_attackBuff -= tempAction.m_attackBuff;
        m_healthBuff -= tempAction.m_healthBuff;
    }

    public override bool ShouldBeRemoved()
    {
        return m_attackBuff <= 0 && m_healthBuff <= 0;
    }

    public override GameUnit GetGameUnit()
    {
        return m_retuningUnit;
    }

    public override JsonGameActionData SaveToJson()
    {
        JsonGameActionData jsonData = new JsonGameActionData
        {
            name = m_name,
            intValue1 = m_attackBuff,
            intValue2 = m_healthBuff
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonGameActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}