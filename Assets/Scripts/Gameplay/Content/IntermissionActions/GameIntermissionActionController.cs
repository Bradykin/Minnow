﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameIntermissionActionController
{
    public GameBuildingIntermission m_building { get; protected set; }
    public GameTechIntermission m_tech { get; protected set; }
    public GameActionIntermission m_action { get; protected set; }

    public GameIntermissionActionController(GameBuildingIntermission building)
    {
        m_building = building;
    }

    public GameIntermissionActionController(GameTechIntermission tech)
    {
        m_tech = tech;
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
        else if (HasTech())
        {
            return m_tech.m_name;
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
            return m_building.m_building.m_desc;
        }
        else if (HasTech())
        {
            return m_tech.m_desc;
        }
        else if (HasAction())
        {
            return m_action.m_desc;
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
            Globals.m_selectedIntermissionBuilding = m_building;
        }

        if (HasTech())
        {
            m_tech.Activate();
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

        if (HasTech())
        {
            return m_tech.CanAfford();
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
        else if (HasTech())
        {
            return m_tech.m_icon;
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
        else if (HasTech())
        {
            return m_tech.m_actionCost;
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
        else if (HasTech())
        {
            return m_tech.m_cost;
        }

        return null;
    }

    public bool HasBuilding()
    {
        return m_building != null;
    }

    public bool HasTech()
    {
        return m_tech != null;
    }

    public bool HasAction()
    {
        return m_action != null;
    }
}