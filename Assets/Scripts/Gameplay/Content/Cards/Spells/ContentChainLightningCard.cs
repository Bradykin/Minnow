using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContentChainLightningCard : GameCardSpellBase
{
    private int m_range = 2;

    public ContentChainLightningCard()
    {
        m_spellEffect = 3;

        m_name = "Chain Lightning";
        m_targetType = Target.Enemy;
        m_cost = 1;
        m_rarity = GameRarity.Common;

        SetupBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.MagicPower, 2);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.DamageSpell);

        m_onPlaySFX = AudioHelper.LightningBolt;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return $"Deal {UIHelper.GetMagicPowerColoredValue(m_spellEffect + mpString)} damage to target enemy, and then chain to up to {UIHelper.GetMagicPowerColoredValue(m_spellEffect + mpString)} enemies in range {m_range} of each other.";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        GameHelper.GetGameController().AddIntermissionLock();

        base.PlayCard(targetUnit);

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(targetUnit.GetGameTile(), m_range, 1);
        List<GameTile> previouslyHitTiles = new List<GameTile>();

        targetUnit.GetHitBySpell(GetSpellValue(), this);

        for (int c = 0; c < GetSpellValue(); c++)
        {
            GameUnit unit = GetNextChainLightningTarget(surroundingTiles, previouslyHitTiles);

            if (unit != null)
            {
                targetUnit = unit;
                previouslyHitTiles.Add(targetUnit.GetGameTile());
                surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(targetUnit.GetGameTile(), m_range, 1);
                targetUnit.GetHitBySpell(GetSpellValue(), this);
            }
            else
            {
                break;
            }
        }

        GameHelper.GetGameController().RemoveIntermissionLock();
    }

    private GameUnit GetNextChainLightningTarget(List<GameTile> surroundingTiles, List<GameTile> previouslyHitTiles)
    {
        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            GameTile temp = surroundingTiles[i];
            int randomIndex = UnityEngine.Random.Range(i, surroundingTiles.Count);
            surroundingTiles[i] = surroundingTiles[randomIndex];
            surroundingTiles[randomIndex] = temp;
        }

        List<GameTile> possibleTargetTiles = surroundingTiles.Where(t => t.IsOccupied() && !t.GetOccupyingUnit().m_isDead && t.GetOccupyingUnit().GetTeam() == Team.Enemy).ToList();

        if (possibleTargetTiles.Count == 0)
        {
            return null;
        }
        else if (possibleTargetTiles.Count == 1)
        {
            return possibleTargetTiles[0].GetOccupyingUnit();
        }
        else
        {
            //Check if there are visible targets. If so, filter out all non-visible
            if (possibleTargetTiles.Any(t => !t.m_isFog))
            {
                possibleTargetTiles = possibleTargetTiles.Where(t => !t.m_isFog).ToList();

                if (possibleTargetTiles.Count == 1)
                {
                    return possibleTargetTiles[0].GetOccupyingUnit();
                }
            }

            //Check if there are targets that have not yet been hit. If so, filter out all that have been previously hit
            if (possibleTargetTiles.Any(t => !previouslyHitTiles.Contains(t)))
            {
                possibleTargetTiles = possibleTargetTiles.Where(t => !previouslyHitTiles.Contains(t)).ToList();

                if (possibleTargetTiles.Count == 1)
                {
                    return possibleTargetTiles[0].GetOccupyingUnit();
                }
            }

            return possibleTargetTiles[Random.Range(0, possibleTargetTiles.Count)].GetOccupyingUnit();
        }
    }
}