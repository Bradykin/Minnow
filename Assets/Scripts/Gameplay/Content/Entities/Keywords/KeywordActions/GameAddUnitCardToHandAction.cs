using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAddUnitCardToHandAction : GameAction
{
    private GameUnit m_gameUnit;

    public GameAddUnitCardToHandAction(GameUnit gameUnit)
    {
        m_gameUnit = gameUnit;

        m_name = "Add unit card to hand";
        m_desc = "Add unit card to hand";
        m_actionParamType = ActionParamType.UnitParam;
    }

    public override void DoAction()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        GameHelper.GetPlayer().AddCardToHand(GameCardFactory.GetCardFromUnit(m_gameUnit), false);
    }

    public override string SaveToJson()
    {
        //TODO: ashulman: This should track the game unit
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
