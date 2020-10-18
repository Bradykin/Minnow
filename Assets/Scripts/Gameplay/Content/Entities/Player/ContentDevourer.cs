using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDevourer : GameUnit
{
    public ContentDevourer()
    {
        m_maxHealth = 20;
        m_maxStamina = 6;
        m_staminaRegen = 4;
        m_power = 5;

        m_team = Team.Player;
        m_rarity = GameRarity.Rare;
        AddKeyword(new GameEnrageKeyword(new GameGainPowerAction(this, 1)), false);
        AddKeyword(new GameVictoriousKeyword(new GameFullHealAction(this)), false);

        m_name = "Devourer";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }
}

public class GameFullHealAction : GameAction
{
    private GameUnit m_unit;

    public GameFullHealAction(GameUnit unit)
    {
        m_unit = unit;

        m_name = "Full Heal";
        m_desc = "Fully heal.";
        m_actionParamType = ActionParamType.UnitParam;
    }

    public override void DoAction()
    {
        m_unit.Heal(m_unit.GetMaxHealth());
    }

    public override string SaveToJson()
    {
        JsonActionData jsonData = new JsonActionData
        {
            name = m_name
        };

        var export = JsonConvert.SerializeObject(jsonData);

        return export;
    }

    public override void LoadFromJson(JsonActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
