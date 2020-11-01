using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class GameRoarOfVictoryAction : GameAction
{
    private GameUnit m_unit;
    private int m_numTimesToTrigger;

    public GameRoarOfVictoryAction(GameUnit unit, int numTimesToTrigger)
    {
        m_unit = unit;
        m_numTimesToTrigger = numTimesToTrigger;

        m_name = "Roar of Victory";
        m_actionParamType = ActionParamType.UnitIntParam;
    }

    public override string GetDesc()
    {
        if (m_numTimesToTrigger == 1)
        {
            return "Trigger all <b>Momentum</b> and <b>Enrage</b> effects on this Unit.";
        }
        else
        {
            return "Trigger all <b>Momentum</b> and <b>Enrage</b> effects on this Unit " + m_numTimesToTrigger + " times.";
        }
    }

    public override void DoAction()
    {
        List<GameMomentumKeyword> momentumKeywords = m_unit.GetKeywords<GameMomentumKeyword>();
        List<GameEnrageKeyword> enrageKeywords = m_unit.GetKeywords<GameEnrageKeyword>();

        for (int c = 0; c < m_numTimesToTrigger; c++)
        {
            for (int i = 0; i < momentumKeywords.Count; i++)
            {
                momentumKeywords[i].DoAction();

                //Repeat the action if the player has the Bestial Wrath relic
                if (m_unit.GetTypeline() == Typeline.Monster && m_unit.GetTeam() == Team.Player)
                {
                    if (GameHelper.HasRelic<ContentBestialWrathRelic>())
                    {
                        momentumKeywords[i].DoAction();
                    }
                }
            }

            for (int i = 0; i < enrageKeywords.Count; i++)
            {
                enrageKeywords[i].DoAction(0);

                //Repeat the action if the player has the Bestial Wrath relic
                if (m_unit.GetTypeline() == Typeline.Monster && m_unit.GetTeam() == Team.Player)
                {
                    if (GameHelper.HasRelic<ContentBestialWrathRelic>())
                    {
                        enrageKeywords[i].DoAction(0);
                    }
                }
            }
        }
    }

    public override void AddAction(GameAction toAdd)
    {
        GameRoarOfVictoryAction tempAction = (GameRoarOfVictoryAction)toAdd;

        m_numTimesToTrigger += tempAction.m_numTimesToTrigger;
    }

    public override void SubtractAction(GameAction toSubtract)
    {
        GameRoarOfVictoryAction tempAction = (GameRoarOfVictoryAction)toSubtract;

        m_numTimesToTrigger -= tempAction.m_numTimesToTrigger;
    }

    public override bool ShouldBeRemoved()
    {
        return m_numTimesToTrigger <= 0;
    }

    public override JsonActionData SaveToJson()
    {
        JsonActionData jsonData = new JsonActionData
        {
            name = m_name,
            intValue1 = m_numTimesToTrigger
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
