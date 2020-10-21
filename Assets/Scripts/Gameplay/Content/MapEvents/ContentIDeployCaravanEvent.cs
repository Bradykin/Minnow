using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDeployCaravanEvent : GameMapEvent
{
    private int m_markerToCheck;

    public ContentDeployCaravanEvent(int markerToCheck)
    {
        m_name = "Deploy Caravan";
        m_desc = "Volcanic eruptions loom on the horizon. Your court had fled to its royal caravan, and needs to run now!";

        m_markerToCheck = markerToCheck;
    }

    public override void TriggerEvent()
    {
        ContentCastleBuilding castleBuilding = GameHelper.GetPlayer().Castle;

        GameHelper.MakePlayerUnit(castleBuilding.GetGameTile(), new ContentRoyalCaravan());

        castleBuilding.GetGameTile().ClearBuilding();
    }
}
