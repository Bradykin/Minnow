using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSabobot : GameUnit
{
    private int m_explosionDamage = 25;
    private int m_explosionRange = 1;
    
    public ContentSabobot()
    {
        m_maxHealth = 1;
        m_maxStamina = 8;
        m_staminaRegen = 2;
        m_power = 1;

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;
        m_keywordHolder.m_keywords.Add(new GameMomentumKeyword(new GameDeathAction(this)));
        m_keywordHolder.m_keywords.Add(new GameDeathKeyword(new GameExplodeAction(this, m_explosionDamage, m_explosionRange)));
        m_keywordHolder.m_keywords.Add(new GameFlyingKeyword());

        m_name = "Sabobot";
        m_desc = "Starts at full Stamina.";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }

    public override void OnSummon()
    {
        base.OnSummon();

        m_curStamina = m_maxStamina;
    }
}

public class GameDeathAction : GameAction
{
    private GameUnit m_unit;

    public GameDeathAction(GameUnit unit)
    {
        m_unit = unit;

        m_name = "Die";
        m_desc = "Die.";
        m_actionParamType = ActionParamType.UnitParam;
    }

    public override void DoAction()
    {
        m_unit.Die();
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
