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
        m_keywordHolder.m_keywords.Add(new GameEnrageKeyword(new GameGainPowerAction(this, 1)));
        m_keywordHolder.m_keywords.Add(new GameVictoriousKeyword(new GameFullHealAction(this)));

        m_name = "Devourer";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }
}

public class GameFullHealAction : GameAction
{
    private GameUnit m_entity;

    public GameFullHealAction(GameUnit entity)
    {
        m_entity = entity;

        m_name = "Full Heal";
        m_desc = "Fully heal.";
        m_actionParamType = ActionParamType.EntityParam;
    }

    public override void DoAction()
    {
        m_entity.Heal(m_entity.GetMaxHealth());
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
