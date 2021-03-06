﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDeployCaravanVolcanoRunEvent : GameMapEvent
{
    private int m_markerToCheck;

    public ContentDeployCaravanVolcanoRunEvent(int markerToCheck)
    {
        m_name = "Deploy Caravan";
        m_desc = "Volcanic eruptions loom on the horizon. Your court has fled to the royal caravan, and needs to run now!";

        m_markerToCheck = markerToCheck;
    }

    public override void TriggerEvent()
    {
        if (GameHelper.GetPlayer().GetCastleGameElement() == null || !(GameHelper.GetPlayer().GetCastleGameElement() is ContentCastleBuilding))
        {
            Debug.LogError("Can't find castle for Deploy Caravan event");
            return;
        }
        
        ContentCastleBuilding castleBuilding = (ContentCastleBuilding)GameHelper.GetPlayer().GetCastleGameElement();

        ContentRoyalCaravan castleUnit = new ContentRoyalCaravan();
        GameHelper.MakePlayerUnit(castleBuilding.GetGameTile(), castleUnit);
        castleUnit.OnSummon();

        castleBuilding.GetGameTile().ClearBuilding();

        GameHelper.GetPlayer().IsUnitCastle = true;
    }
}
