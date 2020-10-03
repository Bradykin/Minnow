﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGainStaminaRangeAction : GameAction
{
    private GameEntity m_entity;
    private int m_toGain;
    private int m_range;

    public GameGainStaminaRangeAction(GameEntity entity, int toGain, int range)
    {
        m_entity = entity;
        m_toGain = toGain;
        m_range = range;

        m_name = "Gain Stamina";
        m_desc = "All friendly units within range " + m_range + " gain " + m_toGain + " Stamina";
        m_actionParamType = ActionParamType.EntityIntParam;
    }

    public override void DoAction()
    {
        List<GameTile> tilesInRange = WorldGridManager.Instance.GetSurroundingTiles(m_entity.GetGameTile(), m_range, 0);
        
        for (int i = 0; i < tilesInRange.Count; i++)
        {
            if (tilesInRange[i].IsOccupied() && !tilesInRange[i].m_occupyingEntity.m_isDead && tilesInRange[i].m_occupyingEntity.GetTeam() == Team.Player)
            {
                tilesInRange[i].m_occupyingEntity.GainStamina(m_toGain);
            }
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