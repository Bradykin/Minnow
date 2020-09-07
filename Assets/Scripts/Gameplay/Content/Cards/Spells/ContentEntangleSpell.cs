using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ContentEntangleSpell : GameCardSpellBase
{
    public ContentEntangleSpell()
    {
        m_name = "Entangle";
        m_desc = "<b>All</b> entities on forests lose all AP.";
        m_playDesc = "Pzaaaaaap!";
        m_targetType = Target.None;
        m_cost = 2;
        m_rarity = GameRarity.Uncommon;

        SetupBasicData();
    }

    public override void PlayCard()
    {
        if (!IsValidToPlay())
        {
            return;
        }

        base.PlayCard();

        WorldTile[] grid = WorldGridManager.Instance.m_gridArray;
        for (int i = 0; i < grid.Length; i++)
        {
            GameTile gameTile = grid[i].GetGameTile();
            if (gameTile.GetTerrain() is ContentForestTerrain)
            {
                if (gameTile.IsOccupied() && !gameTile.m_isFog)
                {
                    gameTile.m_occupyingEntity.SpendAP(gameTile.m_occupyingEntity.GetMaxAP());
                }
            }
        }
    }
}
