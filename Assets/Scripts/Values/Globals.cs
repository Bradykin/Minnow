using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    public static UICard m_selectedCard;
    public static UIEntity m_selectedEntity;
    public static GameBuildingIntermission m_selectedIntermissionBuilding;
    public static bool m_canSelect = true;
    public static bool m_canScroll = true;
    public static bool m_inIntermission = false;

    public static GameTerrainBase m_currentlyPaintingTerrain = new ContentForestTerrain();
}
