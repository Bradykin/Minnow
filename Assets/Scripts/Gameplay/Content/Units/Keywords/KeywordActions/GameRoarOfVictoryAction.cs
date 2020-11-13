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
        GameMomentumKeyword momentumKeyword = m_unit.GetMomentumKeyword();
        GameEnrageKeyword enrageKeyword = m_unit.GetEnrageKeyword();

        for (int c = 0; c < m_numTimesToTrigger; c++)
        {
            if (momentumKeyword != null)
            {
                momentumKeyword.DoAction();
            }

            if (enrageKeyword != null)
            {
                enrageKeyword.DoAction(0);
            }

            //Repeat the action if the player has the Bestial Wrath relic
            if (m_unit.GetTypeline() == Typeline.Monster && m_unit.GetTeam() == Team.Player)
            {
                if (GameHelper.HasRelic<ContentBestialWrathRelic>())
                {
                    if (momentumKeyword != null)
                    {
                        momentumKeyword.DoAction();
                    }

                    if (enrageKeyword != null)
                    {
                        enrageKeyword.DoAction(0);
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

    public override GameUnit GetGameUnit()
    {
        return m_unit;
    }

    public override JsonGameActionData SaveToJson()
    {
        JsonGameActionData jsonData = new JsonGameActionData
        {
            name = m_name,
            intValue1 = m_numTimesToTrigger
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonGameActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
