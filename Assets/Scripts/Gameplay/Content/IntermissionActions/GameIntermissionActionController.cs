using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameIntermissionActionController
{
    public GameBuildingIntermission m_building { get; protected set; }
    public GameActionIntermission m_action { get; protected set; }

    public GameIntermissionActionController(GameBuildingIntermission building)
    {
        m_building = building;
    }

    public GameIntermissionActionController(GameActionIntermission action)
    {
        m_action = action;
    }

    public string GetName()
    {
        if (HasBuilding())
        {
            return m_building.m_building.m_name;
        }
        else if (HasAction())
        {
            return m_action.m_name;
        }

        return "Invalid";
    }

    public string GetDesc()
    {
        if (HasBuilding())
        {
            return m_building.m_building.GetDesc();
        }
        else if (HasAction())
        {
            return m_action.GetDesc();
        }

        return "Invalid";
    }

    public void Activate()
    {
        if (!CanAfford())
        {
            return;
        }

        if (HasBuilding())
        {
            if (Globals.m_selectedIntermissionBuilding == m_building)
            {
                Globals.m_selectedIntermissionBuilding = null;
            }
            else
            {
                Globals.m_selectedIntermissionBuilding = m_building;
            }
        }

        if (HasAction())
        {
            m_action.Activate();
        }
    }

    public bool CanAfford()
    {
        if (HasBuilding())
        {
            return m_building.CanAfford();
        }

        if (HasAction())
        {
            return m_action.CanAfford();
        }

        return false;
    }

    public Sprite GetIcon()
    {
        if (HasBuilding())
        {
            return m_building.m_building.m_icon;
        }
        else if (HasAction())
        {
            return m_action.m_icon;
        }

        return null;
    }

    public int GetActionCost()
    {
        if (HasBuilding())
        {
            return m_building.m_actionCost;
        }
        else if (HasAction())
        {
            return m_action.m_actionCost;
        }

        return 999;
    }

    public GameWallet GetWallet()
    {
        if (HasBuilding())
        {
            return m_building.m_cost;
        }

        return null;
    }

    public bool HasBuilding()
    {
        return m_building != null;
    }

    public bool HasAction()
    {
        return m_action != null;
    }
}
