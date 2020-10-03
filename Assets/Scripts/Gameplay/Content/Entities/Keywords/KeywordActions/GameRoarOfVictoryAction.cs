using System.Collections.Generic;
using UnityEngine;

public class GameRoarOfVictoryAction : GameAction
{
    private GameUnit m_unit;

    public GameRoarOfVictoryAction(GameUnit unit)
    {
        m_unit = unit;

        m_name = "Roar of Victory";
        m_desc = "Trigger all Momentum and Enrage effects on this unit.";
        m_actionParamType = ActionParamType.UnitParam;
    }

    public override void DoAction()
    {
        List<GameMomentumKeyword> momentumKeywords = m_unit.GetKeywords<GameMomentumKeyword>();
        List<GameEnrageKeyword> enrageKeywords = m_unit.GetKeywords<GameEnrageKeyword>();

        int numBestialWrath = GameHelper.RelicCount<ContentBestialWrathRelic>();

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

    public override string SaveToJson()
    {
        JsonActionData jsonData = new JsonActionData
        {
            name = m_name
        };

        var export = JsonUtility.ToJson(jsonData);

        return export;
    }

    public override void LoadFromJson(JsonActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
