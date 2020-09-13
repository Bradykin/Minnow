using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentElvenSentinel : GameEntity
{
    public ContentElvenSentinel()
    {
        m_maxHealth = 2;
        m_maxAP = 4;
        m_apRegen = 1;
        m_power = 1;

        m_team = Team.Player;
        m_rarity = GameRarity.Rare;

        m_keywordHolder.m_keywords.Add(new GameRangeKeyword(2));
        m_keywordHolder.m_keywords.Add(new GameVictoriousKeyword(new GameGainRangeAction(this, 1)));

        m_name = "Elven Sentinel";
        m_desc = "Deal an extra point of damage per tile between " + m_name + " and the target.";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }

    protected override int GetDamageToDealTo(GameEntity other)
    {
        int toDeal = base.GetDamageToDealTo(other);

        toDeal += WorldGridManager.Instance.GetPathLength(m_curTile, other.m_curTile, true, false, true);

        return toDeal;
    }
}

public class GameGainRangeAction : GameAction
{
    private GameEntity m_entity;
    private int m_toGain;

    public GameGainRangeAction(GameEntity entity, int toGain)
    {
        m_entity = entity;
        m_toGain = toGain;

        m_name = "Gain Range";
        m_desc = "+ " + m_toGain + " range";
        m_actionParamType = ActionParamType.EntityIntParam;
    }

    public override void DoAction()
    {
        GameRangeKeyword rangeKeyword = m_entity.GetKeyword<GameRangeKeyword>();

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

        var export = JsonUtility.ToJson(jsonData);

        return export;
    }

    public override void LoadFromJson(JsonActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
