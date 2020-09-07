using System;
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

    public static Type m_currentlyPaintingType = typeof(GameTerrainBase);
    public static GameBuildingBase m_currentlyPaintingBuilding = new ContentCastleBuilding();
    public static GameTerrainBase m_currentlyPaintingTerrain = new ContentForestTerrain();
    public static ContentAngelicGiftEvent m_currentlyPaintingEvent = new ContentAngelicGiftEvent(null);
    public static bool m_inDeckView = false;
}
