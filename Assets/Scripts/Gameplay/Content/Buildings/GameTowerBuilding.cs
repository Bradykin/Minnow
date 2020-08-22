using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTowerBuilding : GameBuildingBase
{
    public int m_power = 5;

    public GameTowerBuilding()
    {
        m_name = "Tower";
        m_desc = "Shoots at enemies on this tile with " + m_power + " power at the start of your turn.";
    }

    public override void EndTurn()
    {
        GameEntity entity = m_tile.m_gameTile.m_occupyingEntity;

        if (entity == null)
        {
            return;
        }

        if (entity.GetTeam() == Team.Player)
        {
            return;
        }

        UIHelper.CreateWorldElementNotification("The " + m_name + " shoots the " + entity.m_name + " for " + m_power + " damage!", true, m_tile);
        entity.Hit(m_power);
    }
}
