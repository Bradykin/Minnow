using System.Collections.Generic;
using UnityEngine;

public class GameEventOrcGoldOption : GameEventOption
{
    private GameTile m_tile;
    private int m_goldAmount = 100;
    private int m_orcsToSpawn = 2;
    private ContentOrcEnemy m_orcCheckerUnit = new ContentOrcEnemy(null);

    public GameEventOrcGoldOption(GameTile tile)
    {
        m_tile = tile;
        m_hasTooltip = true;
    }

    public override string GetMessage()
    {
        m_message = "Go for the small treasure, taking " + m_goldAmount + " gold and waking " + m_orcsToSpawn + " orcs from slumber.";

        return base.GetMessage();
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        player.GainGold(m_goldAmount);

        List<GameTile> nearbyTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_tile, 2);

        List<GameTile> shuffledTiles = new List<GameTile>();
        while (nearbyTiles.Count > 0)
        {
            int index = Random.Range(0, nearbyTiles.Count);
            shuffledTiles.Add(nearbyTiles[index]);
            nearbyTiles.RemoveAt(index);
        }
        int n = shuffledTiles.Count;
        int orcsSpawned = 0;

        for (int i = 0; i < n; i++)
        {
            if (shuffledTiles[i].IsOccupied())
            {
                continue;
            }

            if (!shuffledTiles[i].IsPassable(m_orcCheckerUnit, false))
            {
                continue;
            }

            GameEnemyUnit newOrc = GameUnitFactory.GetEnemyUnitClone(m_orcCheckerUnit, GameHelper.GetOpponent());
            shuffledTiles[i].PlaceUnit(newOrc);
            GameHelper.GetOpponent().m_controlledUnits.Add(newOrc);
            orcsSpawned++;
            if (orcsSpawned >= m_orcsToSpawn)
            {
                break;
            }
        }

        EndEvent();
    }

    //Intentionally left blank
    public override void DeclineOption()
    {

    }

    public override void BuildTooltip()
    {
        UIHelper.CreateUnitTooltip(m_orcCheckerUnit);
    }
}