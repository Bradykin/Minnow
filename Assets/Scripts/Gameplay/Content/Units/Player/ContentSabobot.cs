﻿using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSabobot : GameUnit
{
    private int m_explosionDamage = 25;
    private int m_explosionRange = 1;
    
    public ContentSabobot()
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 1;
        m_maxStamina = 8;
        m_staminaRegen = 2;
        m_power = 1;
        m_startWithMaxStamina = true;

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;
        AddKeyword(new GameMomentumKeyword(new GameDeathAction(this)), false);
        AddKeyword(new GameDeathKeyword(new GameExplodeAction(this, m_explosionDamage, m_explosionRange)), false);
        AddKeyword(new GameFlyingKeyword(), false);

        m_name = "Sabobot";
        m_desc = "Starts at full Stamina.";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }
}

public class GameDeathAction : GameAction
{
    private GameUnit m_unit;
    private int m_numDeaths = 1;

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

    public override void AddAction(GameAction toAdd)
    {
        GameDeathAction tempAction = (GameDeathAction)toAdd;

        m_numDeaths += tempAction.m_numDeaths;
    }

    public override string GetDesc()
    {
        if (m_numDeaths == 1)
        {
            return "Die.";
        }
        else
        {
            return "Die multiple times. It's the same as just once. Die.";
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
