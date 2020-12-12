using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPolarHunter : GameUnit
{
    private int m_powerRange = 2;

    public ContentPolarHunter()
    {
        m_worldTilePositionAdjustment = new Vector3(-0.3f, 0.5f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;

        AddKeyword(new GameRangeKeyword(2), true, false);

        m_name = "Polar Hunter";
        m_desc = $"Has power equal to the total power of all other allied units in range {m_powerRange}.\n";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }

    public override int GetPower()
    {
        int returnPower = 0;

        if (GameHelper.IsUnitInWorld(this))
        {
            List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(GetGameTile(), m_powerRange, 1);

            for (int i = 0; i < surroundingTiles.Count; i++)
            {
                GameUnit unit = surroundingTiles[i].GetOccupyingUnit();

                if (unit != null && !unit.m_isDead && unit.GetTeam() == Team.Player)
                {
                    returnPower += unit.GetPower();
                }
            }
        }

        return returnPower;
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 10;
        m_maxStamina = 5;
        m_staminaRegen = 2;
        m_power = 0;
    }
}