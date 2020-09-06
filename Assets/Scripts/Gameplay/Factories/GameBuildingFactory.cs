using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameBuildingFactory
{
    private static List<GameBuildingBase> m_buildings = new List<GameBuildingBase>();

    private static bool hasInit = false;

    public static void Init()
    {
        m_buildings.Add(new ContentCastleBuilding());
        m_buildings.Add(new ContentEmberForgeBuilding());
        m_buildings.Add(new ContentFarmBuilding());
        m_buildings.Add(new ContentForestLodgeBuilding());
        m_buildings.Add(new ContentFortressBuilding());
        m_buildings.Add(new ContentGraveyardBuilding());
        m_buildings.Add(new ContentInnBuilding());
        m_buildings.Add(new ContentMagicSchoolBuilding());
        m_buildings.Add(new ContentMineBuilding());
        m_buildings.Add(new ContentSmithyBuilding());
        m_buildings.Add(new ContentTempleBuilding());
        hasInit = true;
    }

    public static GameBuildingBase GetBuildingClone(GameBuildingBase building)
    {
        return (GameBuildingBase)Activator.CreateInstance(building.GetType());
    }

    public static GameBuildingBase GetTerrainFromJson(JsonGameBuildingData jsonData)
    {
        int i = m_buildings.FindIndex(t => t.m_name == jsonData.name);

        GameBuildingBase newBuilding = (GameBuildingBase)Activator.CreateInstance(m_buildings[i].GetType());
        newBuilding.LoadFromJson(jsonData);

        return newBuilding;
    }
}
