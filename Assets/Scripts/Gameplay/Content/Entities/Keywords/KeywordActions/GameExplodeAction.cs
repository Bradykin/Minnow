using System.Collections.Generic;
using UnityEngine;

public class GameExplodeAction : GameAction
{
    private GameUnit m_explodingUnit;
    private int m_explodePower;
    private int m_explodeRange;

    public GameExplodeAction(GameUnit explodingUnit, int explodePower, int explodeRange)
    {
        m_explodingUnit = explodingUnit;
        m_explodePower = explodePower;
        m_explodeRange = explodeRange;

        m_name = "Explode";
        m_desc = "Explode for " + m_explodePower + " damage to all units and buildings in range " + m_explodeRange;
        m_actionParamType = ActionParamType.UnitTwoIntParam;
    }

    public override void DoAction()
    {
        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingTiles(m_explodingUnit.GetGameTile(), m_explodeRange, 1);

        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            GameBuildingBase building = surroundingTiles[i].GetBuilding();
            GameUnit unit = surroundingTiles[i].m_occupyingUnit;

            if (building != null)
            {
                building.GetHit(m_explodePower);
            }

            if (unit != null)
            {
                unit.GetHit(m_explodePower);
            }
        }
    }

    public override string SaveToJson()
    {
        JsonActionData jsonData = new JsonActionData
        {
            name = m_name,
            intValue1 = m_explodePower,
            intValue2 = m_explodeRange
        };

        var export = JsonUtility.ToJson(jsonData);

        return export;
    }

    public override void LoadFromJson(JsonActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
