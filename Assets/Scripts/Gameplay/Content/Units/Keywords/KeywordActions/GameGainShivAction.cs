using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGainShivAction : GameAction
{
    private int m_toAdd = 1;

    public GameGainShivAction(int toAdd)
    {
        m_toAdd = toAdd;

        m_name = "Gain Shiv";
        m_actionParamType = ActionParamType.IntParam;
    }

    public override string GetDesc()
    {
        if (m_toAdd == 1)
        {
            return "Add a shiv to your hand.";
        }
        else
        {
            return "Add " + m_toAdd + " shivs to your hand.";
        }
    }

    public override void DoAction()
    {
        for (int i = 0; i < m_toAdd; i++)
        {
            GameHelper.GetPlayer().AddCardToHand(GameCardFactory.GetCardClone(new ContentShivCard()), false);
        }
    }

    public override void AddAction(GameAction toAdd)
    {
        GameGainShivAction tempAction = (GameGainShivAction)toAdd;

        m_toAdd += tempAction.m_toAdd;
    }

    public override void SubtractAction(GameAction toSubtract)
    {
        GameGainShivAction tempAction = (GameGainShivAction)toSubtract;

        m_toAdd -= tempAction.m_toAdd;
    }

    public override bool ShouldBeRemoved()
    {
        return m_toAdd <= 0;
    }

    public override GameUnit GetGameUnit()
    {
        return null;
    }

    public override JsonGameActionData SaveToJson()
    {
        JsonGameActionData jsonData = new JsonGameActionData
        {
            name = m_name,
            intValue1 = m_toAdd
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonGameActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
