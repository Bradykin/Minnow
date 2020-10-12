using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentElvenSentinel : GameUnit
{
    public ContentElvenSentinel()
    {
        m_maxHealth = 6;
        m_maxStamina = 5;
        m_staminaRegen = 3;
        m_power = 1;

        m_team = Team.Player;
        m_rarity = GameRarity.Rare;

        m_keywordHolder.m_keywords.Add(new GameRangeKeyword(3));
        m_keywordHolder.m_keywords.Add(new GameVictoriousKeyword(new GameGainRangeAction(this, 1)));

        m_name = "Elven Sentinel";
        m_desc = "Deal an extra point of damage per tile between " + m_name + " and the target unit.";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }

    protected override int GetDamageToDealTo(GameUnit other)
    {
        int toDeal = base.GetDamageToDealTo(other);

        toDeal += WorldGridManager.Instance.GetPathLength(m_gameTile, other.GetGameTile(), true, false, true);

        return toDeal;
    }
}

public class GameGainRangeAction : GameAction
{
    private GameUnit m_unit;
    private int m_toGain;

    public GameGainRangeAction(GameUnit unit, int toGain)
    {
        m_unit = unit;
        m_toGain = toGain;

        m_name = "Gain Range";
        m_desc = "+ " + m_toGain + " range";
        m_actionParamType = ActionParamType.UnitIntParam;
    }

    public override void DoAction()
    {
        GameRangeKeyword rangeKeyword = m_unit.GetKeyword<GameRangeKeyword>();

        if (rangeKeyword != null)
        {
            rangeKeyword.IncreaseRange(m_toGain);
        }
    }

    public override string SaveToJson()
    {
        JsonActionData jsonData = new JsonActionData
        {
            name = m_name,
            intValue1 = m_toGain
        };

        var export = JsonConvert.SerializeObject(jsonData);

        return export;
    }

    public override void LoadFromJson(JsonActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
