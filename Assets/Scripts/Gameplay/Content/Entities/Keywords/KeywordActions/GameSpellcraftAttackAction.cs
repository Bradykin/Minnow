using UnityEngine;

public class GameSpellcraftAttackAction : GameAction
{
    private GameEntity m_gameEntity;

    public GameSpellcraftAttackAction(GameEntity gameEntity)
    {
        m_gameEntity = gameEntity;

        m_name = "Spellcraft";
        m_desc = "Spellcraft!";
        m_actionParamType = ActionParamType.EntityParam;
    }

    public override void DoAction()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        player.TriggerSpellcraft(GameCard.Target.Unit, m_gameEntity.GetGameTile());
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