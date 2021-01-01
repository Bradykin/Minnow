using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentManaLocusBuilding : GameBuildingBase
{
    public ContentManaLocusBuilding()
    {
        m_range = 3;

        m_name = "Mana Locus";
        m_desc = "";
        m_rarity = GameRarity.Uncommon;
        m_buildingType = BuildingType.Economic;

        m_maxHealth = 20;
        m_cost = new GameWallet(200);

        m_expandsPlaceRange = false;
        m_spellcraftBuilding = true;

        LateInit();
    }

    public override string GetDesc()
    {
        m_desc = $"<b>Spellcraft</b>: Give all allied unitw in range {m_range} +1/+1.";

        return m_desc;
    }

    public override void OnSpellCraft()
    {
        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(GetGameTile(), m_range, 0);

        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            if (surroundingTiles[i].IsOccupied() && surroundingTiles[i].GetOccupyingUnit().GetTeam() == GetTeam())
            {
                surroundingTiles[i].GetOccupyingUnit().AddStats(1, 1, false, true);
            }
        }
    }

    public override bool IsValidTerrainToPlace(GameTerrainBase terrain, GameTile tile)
    {
        if (terrain.IsPlains())
        {
            return true;
        }

        return false;
    }
}
