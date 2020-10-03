using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHero : GameUnit
{
    public ContentHero()
    {
        m_maxHealth = 50;
        m_maxStamina = 6;
        m_staminaRegen = 4;
        m_power = 6;

        m_team = Team.Player;
        m_rarity = GameRarity.Rare;

        m_keywordHolder.m_keywords.Add(new GameEnrageKeyword(new GameGainPowerAction(this, 1)));
        m_keywordHolder.m_keywords.Add(new GameMomentumKeyword(new GameHealAction(this, 5)));
        m_keywordHolder.m_keywords.Add(new GameVictoriousKeyword(new GameGainResourceAction(new GameWallet(15))));

        m_name = "Hero";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }
}

public class GameHealAction : GameAction
{
    private GameUnit m_entity;
    private int m_healVal;

    public GameHealAction(GameUnit entity, int healVal)
    {
        m_entity = entity;
        m_healVal = healVal;

        m_name = "Heal";
        m_desc = "Heal for " + healVal + ".";
        m_actionParamType = ActionParamType.EntityIntParam;
    }

    public override void DoAction()
    {
        m_entity.Heal(m_healVal);
    }

    public override string SaveToJson()
    {
        JsonActionData jsonData = new JsonActionData
        {
            name = m_name,
            intValue1 = m_healVal
        };

        var export = JsonUtility.ToJson(jsonData);

        return export;
    }

    public override void LoadFromJson(JsonActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
