using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAddEntityCardToHandAction : GameAction
{
    private GameEntity m_gameEntity;

    public GameAddEntityCardToHandAction(GameEntity gameEntity)
    {
        m_gameEntity = gameEntity;

        m_name = "Add unit card to hand";
        m_desc = "Add unit card to hand";
        m_actionParamType = ActionParamType.EntityParam;
    }

    public override void DoAction()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        GameHelper.GetPlayer().AddCardToHand(GameCardFactory.GetCardFromEntity(m_gameEntity), false);
    }

    public override string SaveToJson()
    {
        //TODO: ashulman: This should track the gameentity
        JsonActionData jsonData = new JsonActionData
        {
            name = m_name,
        };

        var export = JsonUtility.ToJson(jsonData);

        return export;
    }

    public override void LoadFromJson(JsonActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
