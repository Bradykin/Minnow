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
        m_desc = "Target allied <b>Humanoid</b> unit gets +" + m_buffNum + "/+" + m_buffNum + " for each allied <b>Humanoid</b> unit within " + m_effectRange + " tiles (including itself).";
        m_targetType = Target.Ally;
        m_cost = 1;
        m_rarity = GameRarity.Common;

        SetupBasicData();

        m_audioCategory = AudioHelper.SpellAudioCategory.Buff;
    }

    public override bool IsValidToPlay(GameUnit targetUnit)
    {
        return base.IsValidToPlay(targetUnit) && targetUnit.GetTypeline() == Typeline.Humanoid;
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
                    surroundingTiles[i].GetOccupyingUnit().GetTeam() == Team.Player && surroundingTiles[i].GetOccupyingUnit().GetTypeline() == Typeline.Humanoid)
                {
                    amount+=1;
                }
            }
        }

        targetUnit.AddStats(amount * m_buffNum, amount * m_buffNum, false, true);
    }
}