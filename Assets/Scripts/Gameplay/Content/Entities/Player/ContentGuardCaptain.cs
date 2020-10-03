using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGuardCaptain : GameUnit
{
    private int m_rallyRange = 2;
    private int m_rallyValue = 3;

    public ContentGuardCaptain()
    {
        m_maxHealth = 12;
        m_maxStamina = 5;
        m_staminaRegen = 2;
        m_power = 4;

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        m_name = "Guard Captain";
        m_desc = "When summoned, all allied <b>Humanoid</b> units within range " + m_rallyRange + " gain +" + m_rallyValue + " Stamina.";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }

    public override void OnSummon()
    {
        base.OnSummon();
        
        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingTiles(m_gameTile, m_rallyRange, 0);

        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            GameUnit entity = surroundingTiles[i].m_occupyingUnit;

            if (entity == null)
            {
                continue;
            }

            if (entity.GetTeam() != Team.Player)
            {
                continue;
            }

            if (entity.GetTypeline() != Typeline.Humanoid)
            {
                continue;
            }

            entity.GainStamina(m_rallyValue);
        }
    }
}