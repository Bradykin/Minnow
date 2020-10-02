using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPhalanxCard : GameCardSpellBase
{
    private int m_effectRange = 2;
    
    public ContentPhalanxCard()
    {
        m_name = "Phalanx";
        m_desc = "Target allied <b>Humanoid</b> unit gets +1/+1 for each allied <b>Humanoid</b> unit within " + m_effectRange + " tiles (including itself).";
        m_targetType = Target.Ally;
        m_cost = 1;
        m_rarity = GameRarity.Common;

        SetupBasicData();
    }

    public override bool IsValidToPlay(GameEntity targetEntity)
    {
        return base.IsValidToPlay() && targetEntity.GetTypeline() == Typeline.Humanoid;
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        GameTile tile = targetEntity.GetGameTile();

        int amount = 0;
        if (tile != null)
        {
            List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingTiles(tile, m_effectRange, 0);

            for (int i = 0; i < surroundingTiles.Count; i++)
            {
                if (surroundingTiles[i].IsOccupied() && !surroundingTiles[i].m_occupyingEntity.m_isDead &&
                    surroundingTiles[i].m_occupyingEntity.GetTeam() == Team.Player && surroundingTiles[i].m_occupyingEntity.GetTypeline() == Typeline.Humanoid)
                {
                    amount+=1;
                }
            }
        }

        targetEntity.AddPower(amount);
        targetEntity.AddMaxHealth(amount);
    }
}