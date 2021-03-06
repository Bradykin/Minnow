﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameBuildingFactory
{
    private static List<GameBuildingBase> m_buildings = new List<GameBuildingBase>();

    private static bool m_hasInit = false;

    public static void Init()
    {
        m_buildings.Add(new ContentCastleBuilding());
        m_buildings.Add(new ContentWizardTowerBuilding());
        m_buildings.Add(new ContentForestLodgeBuilding());
        m_buildings.Add(new ContentFortressBuilding());
        m_buildings.Add(new ContentFarmlandBuilding());
        m_buildings.Add(new ContentFrontierTownBuilding());
        m_buildings.Add(new ContentMagicSchoolBuilding());
        m_buildings.Add(new ContentMineBuilding());
        m_buildings.Add(new ContentSmithyBuilding());
        m_buildings.Add(new ContentTempleBuilding());

        m_buildings.Add(new ContentHillFortBuilding());
        m_buildings.Add(new ContentManaLocusBuilding());
        m_buildings.Add(new ContentMountainGatewayBuilding());
        m_buildings.Add(new ContentRingOfProtectionBuilding());

        //Enemy Buildings
        m_buildings.Add(new ContentPowerCrystalBuilding());

        m_hasInit = true;
    }

    public static GameBuildingBase GetPreviousBuilding(GameBuildingBase currentBuilding)
    {
        if (!m_hasInit)
            Init();

        int r = m_buildings.FindIndex(t => t.GetType() == currentBuilding.GetType());

        if (r == 0)
            r = m_buildings.Count - 1;
        else
            r--;

        return (GameBuildingBase)Activator.CreateInstance(m_buildings[r].GetType());
    }

    public static GameBuildingBase GetNextBuilding(GameBuildingBase currentBuilding)
    {
        if (!m_hasInit)
            Init();

        int r = m_buildings.FindIndex(t => t.GetType() == currentBuilding.GetType());

        if (r == m_buildings.Count - 1)
            r = 0;
        else
            r++;

        return (GameBuildingBase)Activator.CreateInstance(m_buildings[r].GetType());
    }

    public static GameBuildingBase GetBuildingClone(GameBuildingBase building)
    {
        if (!m_hasInit)
            Init();

        return (GameBuildingBase)Activator.CreateInstance(building.GetType());
    }

    public static GameBuildingBase GetBuildingFromJson(JsonGameBuildingData jsonData)
    {
        if (!m_hasInit)
            Init();

        int i = m_buildings.FindIndex(t => t.GetName() == jsonData.name);

        GameBuildingBase newBuilding = (GameBuildingBase)Activator.CreateInstance(m_buildings[i].GetType());
        newBuilding.LoadFromJson(jsonData);

        return newBuilding;
    }
}
