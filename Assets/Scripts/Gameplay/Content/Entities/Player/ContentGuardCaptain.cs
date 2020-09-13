using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGuardCaptain : GameEntity
{
    private int m_rallyRange = 2;
    private int m_rallyValue = 3;

    public ContentGuardCaptain()
    {
        m_maxHealth = 12;
        m_maxAP = 5;
        m_apRegen = 2;
        m_power = 4;

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        m_name = "Guard Captain";
        m_desc = "When summoned, gives all friendly entites within " + m_rallyRange + " range " + m_rallyValue + " current AP";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }

    public override void OnSummon()
    {
        base.OnSummon();
        
        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingTiles(m_curTile, m_rallyRange, 0);

        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            GameEntity entity = surroundingTiles[i].m_occupyingEntity;

            if (entity == null)
            {
                continue;
            }

            if (entity.GetTeam() != Team.Player)
            {
                continue;
            }

            entity.GainAP(m_rallyValue);
        }
    }
}