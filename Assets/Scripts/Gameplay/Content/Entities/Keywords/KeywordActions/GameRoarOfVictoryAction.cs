using System.Collections.Generic;
using UnityEngine;

public class GameRoarOfVictoryAction : GameAction
{
    private GameEntity m_entity;

    public GameRoarOfVictoryAction(GameEntity entity)
    {
        m_entity = entity;

        m_name = "Roar of Victory";
        m_desc = "Trigger all Momentum and Enrage effects on this entity";
        m_actionParamType = ActionParamType.EntityParam;
    }

    public override void DoAction()
    {
        List<GameMomentumKeyword> momentumKeywords = m_entity.GetKeywords<GameMomentumKeyword>();
        List<GameEnrageKeyword> enrageKeywords = m_entity.GetKeywords<GameEnrageKeyword>();

        for (int i = 0; i < momentumKeywords.Count; i++)
        {
            momentumKeywords[i].DoAction();
        }

        for (int i = 0; i < enrageKeywords.Count; i++)
        {
            enrageKeywords[i].DoAction(0);
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
