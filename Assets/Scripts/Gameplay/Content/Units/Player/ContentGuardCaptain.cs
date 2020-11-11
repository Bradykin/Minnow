using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGuardCaptain : GameUnit
{
    private int m_rallyRange = 2;
    private int m_rallyValue = 3;

    public ContentGuardCaptain()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.2f, 0);

        m_maxHealth = 12;
        m_maxStamina = 5;
        m_staminaRegen = 2;
        m_power = 4;

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        m_name = "Guard Captain";
        m_desc = "When summoned, all allied <b>Humanoid</b> units within range " + m_rallyRange + " gain +" + m_rallyValue + " Stamina.\n";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }

    public override void OnSummon()
    {
        base.OnSummon();
        
        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_gameTile, m_rallyRange, 0);

        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            GameUnit unit = surroundingTiles[i].m_occupyingUnit;

            if (unit == null)
            {
                continue;
            }

            if (unit.GetTeam() != Team.Player)
            {
                continue;
            }

            if (unit.GetTypeline() != Typeline.Humanoid)
            {
                continue;
            }

            unit.GainStamina(m_rallyValue);
        }
    }
}