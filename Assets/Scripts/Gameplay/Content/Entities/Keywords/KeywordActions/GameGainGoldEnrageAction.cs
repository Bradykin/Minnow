﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGainGoldEnrageAction : GameAction
{
    private GameUnit m_unit;

    public GameGainGoldEnrageAction(GameUnit unit)
    {
        m_unit = unit;

        m_name = "Gain Gold Enrage";
        m_desc = "Gain gold equal to the damage taken.";
        m_actionParamType = ActionParamType.UnitParam;
    }

    public override void DoAction()
    {

    }

    public void DoAction(int damageAmount)
    {
        GamePlayer player = GameHelper.GetPlayer();

        player.m_wallet.m_gold += damageAmount;
    }

    public override string SaveToJson()
    {
        JsonActionData jsonData = new JsonActionData
        {
            name = m_name,
        };

        var export = JsonUtility.ToJson(jsonData);

        return export;
    }

    public override void LoadFromJson(JsonActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
