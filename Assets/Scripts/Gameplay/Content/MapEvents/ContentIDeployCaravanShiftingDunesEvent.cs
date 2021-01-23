using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDeployCaravanShiftingDunesEvent : GameMapEvent
{
    private int m_markerToCheck;

    public ContentDeployCaravanShiftingDunesEvent(int markerToCheck)
    {
        m_name = "Deploy Caravan";
        m_desc = "No sane person would build a castle in this desert. Get in your caravan and navigate the dunes!";
        m_triggerType = ScheduledActionTime.StartOfWave;

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
