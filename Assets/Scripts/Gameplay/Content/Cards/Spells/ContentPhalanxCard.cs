using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPhalanxCard : GameCardSpellBase
{
    private int m_effectRange = 2;
    private int m_buffNum = 2;
    
    public ContentPhalanxCard()
    {
        m_name = "Phalanx";
        m_desc = "Target allied unit gets +" + m_buffNum + "/+" + m_buffNum + " for each allied unit within " + m_effectRange + " tiles (including itself).";
        m_targetType = Target.Ally;
        m_cost = 1;
        m_rarity = GameRarity.Common;

        SetupBasicData();

        m_onPlaySFX = AudioHelper.SmallBuff;
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        GameTile tile = targetUnit.GetGameTile();

        int amount = 0;
        if (tile != null)
        {
            List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(tile, m_effectRange, 0);

            for (int i = 0; i < surroundingTiles.Count; i++)
            {
                if (surroundingTiles[i].IsOccupied() && !surroundingTiles[i].GetOccupyingUnit().m_isDead &&
                    surroundingTiles[i].GetOccupyingUnit().GetTeam() == Team.Player)
                {
                    amount+=1;
                }
            }
        }

        targetUnit.AddStats(amount * m_buffNum, amount * m_buffNum, false, true);
    }
}