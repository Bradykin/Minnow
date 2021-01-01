using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPhalanxCard : GameCardSpellBase
{
    private int m_buffNum = 1;
    
    public ContentPhalanxCard()
    {
        m_name = "Phalanx";
        m_targetType = Target.Ally;
        m_cost = 2;
        m_rarity = GameRarity.Common;
        m_spellEffect = 1;

        SetupBasicData();

        m_onPlaySFX = AudioHelper.SmallBuff;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return $"Target allied unit gets +{m_buffNum}/+{m_buffNum} for each allied unit within {UIHelper.GetMagicPowerColoredValue(m_spellEffect + mpString)} tiles (including itself).";
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
            List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(tile, GetSpellValue(), 0);

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