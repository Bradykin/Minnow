using UnityEngine;

public class GameSpellcraftAttackAction : GameAction
{
    private GameUnit m_gameUnit;

    public GameSpellcraftAttackAction(GameUnit gameUnit)
    {
        m_gameUnit = gameUnit;

        m_name = "Spellcraft";
        m_desc = "Spellcraft!";
        m_actionParamType = ActionParamType.UnitParam;
    }

    public override void DoAction()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        player.TriggerSpellcraft(GameCard.Target.Unit, m_gameUnit.GetGameTile());
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