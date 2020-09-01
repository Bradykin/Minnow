using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameBuildingFactory
{
    public static GameBuildingBase GetBuildingClone(GameBuildingBase building)
    {
        return (GameBuildingBase)Activator.CreateInstance(building.GetType());
    }
}
