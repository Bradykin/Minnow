using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGainAPRangeAction : GameAction
{
    private GameEntity m_entity;
    private int m_toGain;
    private int m_range;

    public GameGainAPRangeAction(GameEntity entity, int toGain, int range)
    {
        m_entity = entity;
        m_toGain = toGain;
        m_range = range;

        m_name = "Gain AP";
        m_desc = "+ " + m_toGain + " AP";
        m_actionParamType = ActionParamType.EntityIntParam;
    }

    public override void DoAction()
    {
        List<GameTile> tilesInRange = WorldGridManager.Instance.GetSurroundingTiles(m_entity.GetGameTile(), m_range, 0);
        
        for (int i = 0; i < tilesInRange.Count; i++)
        {
            if (tilesInRange[i].IsOccupied() && !tilesInRange[i].m_occupyingEntity.m_isDead && tilesInRange[i].m_occupyingEntity.GetTeam() == Team.Player)
            {
                tilesInRange[i].m_occupyingEntity.GainAP(m_toGain);
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
