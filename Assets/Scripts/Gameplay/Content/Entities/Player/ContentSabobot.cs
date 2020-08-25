using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSabobot : GameEntity
{
    public ContentSabobot()
    {
        m_maxHealth = 1;
        m_maxAP = 4;
        m_apRegen = 4;
        m_power = 25;

        m_team = Team.Player;
        m_rarity = GameRarity.Common;
        m_keywordHolder.m_keywords.Add(new GameMomentumKeyword(new GameDeathAction(this)));

        m_name = "Sabobot";
        m_typeline = Typeline.Construct;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
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
    }

    public override void DoAction()
    {
        m_entity.Die();
    }
}
