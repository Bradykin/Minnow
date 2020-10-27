using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class GameRoarOfVictoryAction : GameAction
{
    private GameUnit m_unit;
    private int m_numToTrigger;

    public GameRoarOfVictoryAction(GameUnit unit)
    {
        m_unit = unit;

        m_name = "Roar of Victory";
        m_actionParamType = ActionParamType.UnitParam;
    }

    public override void DoAction()
    {
        List<GameMomentumKeyword> momentumKeywords = m_unit.GetKeywords<GameMomentumKeyword>();
        List<GameEnrageKeyword> enrageKeywords = m_unit.GetKeywords<GameEnrageKeyword>();

        int numBestialWrath = GameHelper.RelicCount<ContentBestialWrathRelic>();

        for (int c = 0; c < m_numToTrigger; c++)
        {
            for (int i = 0; i < momentumKeywords.Count; i++)
            {
                momentumKeywords[i].DoAction();
                for (int k = 0; k < numBestialWrath; k++)
                {
                    momentumKeywords[i].DoAction();
                }
            }

            for (int i = 0; i < enrageKeywords.Count; i++)
            {
                enrageKeywords[i].DoAction(0);
                for (int k = 0; k < numBestialWrath; k++)
                {
                    enrageKeywords[i].DoAction(0);
                }
            }
        }
    }

    public override void AddAction(GameAction toAdd)
    {
        GameRoarOfVictoryAction tempAction = (GameRoarOfVictoryAction)toAdd;

        m_numToTrigger += tempAction.m_numToTrigger;
    }

    public override string GetDesc()
    {
        if (m_numToTrigger == 1)
        {
            return "Trigger all <b>Momentum</b> and <b>Enrage</b> effects on this Unit.";
        }
        else
        {
            return "Trigger all <b>Momentum</b> and <b>Enrage</b> effects on this Unit " + m_numToTrigger + " times.";
        }
    }

    public override JsonActionData SaveToJson()
    {
        JsonActionData jsonData = new JsonActionData
        {
            name = m_name
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
