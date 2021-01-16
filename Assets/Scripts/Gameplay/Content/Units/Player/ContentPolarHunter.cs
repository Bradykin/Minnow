using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPolarHunter : GameUnit
{
    public ContentPolarHunter() : base()
    {
        m_worldTilePositionAdjustment = new Vector3(-0.3f, 0.5f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;
        m_aoeRange = 2;

        AddKeyword(new GameRangeKeyword(2), true, false);

        m_name = "Polar Hunter";
        m_desc = $"Has attack equal to the total attack of all other allied units in range {m_aoeRange}.\n";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }

    public override AudioClip GetAttackSFX()
    {
        if (GetAttack() >= 30)
        {
            return AudioHelper.SpearHeavy;
        }

        return AudioHelper.SpearLight;
    }

    public override int GetAttack()
    {
        int returnAttack = 0;

        if (GameHelper.IsUnitInWorld(this))
        {
            List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(GetGameTile(), m_aoeRange, 1);

            for (int i = 0; i < surroundingTiles.Count; i++)
            {
                GameUnit unit = surroundingTiles[i].GetOccupyingUnit();

                if (unit is ContentPolarHunter)
                {
                    continue;
                }

                if (unit != null && !unit.m_isDead && unit.GetTeam() == Team.Player)
                {
                    returnAttack += unit.GetAttack();
                }
            }
        }

        return returnAttack;
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 10;
        m_maxStamina = 5;
        m_staminaRegen = 3;
        m_attack = 0;
    }
}