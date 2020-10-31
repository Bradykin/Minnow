using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHero : GameUnit
{
    public ContentHero()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.4f, 0);

        m_maxHealth = 50;
        m_maxStamina = 6;
        m_staminaRegen = 4;
        m_power = 6;

        m_team = Team.Player;
        m_rarity = GameRarity.Rare;

        AddKeyword(new GameEnrageKeyword(new GameGainStatsAction(this, 1, 0)), false);
        AddKeyword(new GameMomentumKeyword(new GameHealAction(this, 5)), false);
        AddKeyword(new GameVictoriousKeyword(new GameGainResourceAction(new GameWallet(15))), false);

        m_name = "Hero";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }
}

public class GameHealAction : GameAction
{
    private GameUnit m_unit;
    private int m_healVal;

    public GameHealAction(GameUnit unit, int healVal)
    {
        m_unit = unit;
        m_healVal = healVal;

        m_name = "Heal";
        m_actionParamType = ActionParamType.UnitIntParam;
    }

    public override void DoAction()
    {
        m_unit.Heal(m_healVal);
    }

    public override void AddAction(GameAction toAdd)
    {
        GameHealAction tempAction = (GameHealAction)toAdd;

        m_healVal += tempAction.m_healVal;
    }

    public override string GetDesc()
    {
        return "Heal for " + m_healVal + ".";
    }

    public override JsonActionData SaveToJson()
    {
        JsonActionData jsonData = new JsonActionData
        {
            name = m_name,
            intValue1 = m_healVal
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
