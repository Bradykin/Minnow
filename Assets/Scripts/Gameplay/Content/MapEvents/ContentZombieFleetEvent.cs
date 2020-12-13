using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentZombieFleetEvent : GameMapEvent
{
    private int m_markerToCheck;

    public ContentZombieFleetEvent(int markerToCheck)
    {
        m_name = "Zombie Fleet";
        m_desc = "A zombie fleet has arrived on the shores!";

        m_triggerType = ScheduledActionTime.StartOfWave;
        m_markerToCheck = markerToCheck;
    }

    public override void TriggerEvent()
    {
        List<GameTile> eventTiles = WorldGridManager.Instance.GetTilesWithEventMarker(m_markerToCheck);
        for (int i = 0; i < eventTiles.Count; i++)
        {
            GameEnemyUnit newEnemyUnit = GameUnitFactory.GetEnemyUnitClone(new ContentZombieShipEnemy(WorldController.Instance.m_gameController.m_gameOpponent));
            eventTiles[i].PlaceUnit(newEnemyUnit);
            newEnemyUnit.OnSummon();
            WorldController.Instance.m_gameController.m_gameOpponent.AddControlledUnit(newEnemyUnit);
        }
    }
}
