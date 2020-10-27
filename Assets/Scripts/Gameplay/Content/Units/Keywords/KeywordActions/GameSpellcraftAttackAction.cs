using Newtonsoft.Json;
using UnityEngine;

public class GameSpellcraftAttackAction : GameAction
{
    private GameUnit m_gameUnit;
    private int m_numSpellcraft = 1;

    public GameSpellcraftAttackAction(GameUnit gameUnit)
    {
        m_gameUnit = gameUnit;

        m_name = "Spellcraft";
        m_actionParamType = ActionParamType.UnitParam;
    }

    public override void DoAction()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        for (int i = 0; i < m_numSpellcraft; i++)
        {
            player.TriggerSpellcraft(GameCard.Target.Unit, m_gameUnit.GetGameTile());
        }
    }

    public override void AddAction(GameAction toAdd)
    {
        GameSpellcraftAttackAction tempAction = (GameSpellcraftAttackAction)toAdd;

        m_numSpellcraft += tempAction.m_numSpellcraft;
    }

    public override string GetDesc()
    {
        if (m_numSpellcraft == 1)
        {
            return "Trigger <b>Spellcraft</b>.";
        }
        else
        {
            return "Trigger <b>Spellcraft</b> " + m_numSpellcraft + " times.";
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