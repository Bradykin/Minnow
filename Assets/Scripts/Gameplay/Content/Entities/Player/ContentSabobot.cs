using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSabobot : GameEntity
{
    private int m_explosionDamage = 25;
    private int m_explosionRange = 1;
    
    public ContentSabobot()
    {
        m_maxHealth = 1;
        m_maxAP = 8;
        m_apRegen = 2;
        m_power = 1;

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;
        m_keywordHolder.m_keywords.Add(new GameMomentumKeyword(new GameDeathAction(this)));
        m_keywordHolder.m_keywords.Add(new GameDeathKeyword(new GameExplodeAction(this, m_explosionDamage, m_explosionRange)));

        m_name = "Sabobot";
        m_desc = "Starts at max AP.";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }

    public override void OnSummon()
    {
        base.OnSummon();

        m_curAP = m_maxAP;
    }
}

public class GameDeathAction : GameAction
{
    private GameEntity m_entity;

    public GameDeathAction(GameEntity entity)
    {
        m_entity = entity;

        m_name = "Die";
        m_desc = "Die.";
        m_actionParamType = ActionParamType.EntityParam;
    }

    public override void DoAction()
    {
        m_entity.Die();
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
