using System.Collections;
using System.Collections.Generic;
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

        m_tags.AddTag(GameTag.TagType.MagicPower);

        m_audioCategory = AudioHelper.SpellAudioCategory.Damage;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return "Deal "  + m_spellEffect + mpString + " damage to target enemy, and then chain to up to " + m_spellEffect + mpString + " enemies in range " + m_range + " of each other.";
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

        targetUnit.GetHitBySpell(GetSpellValue(), this);

        for (int c = 0; c < GetSpellValue(); c++)
        {
            for (int i = 0; i < surroundingTiles.Count; i++)
            {
                GameUnit unit = surroundingTiles[i].GetOccupyingUnit();

                if (unit != null && !unit.m_isDead && unit.GetTeam() == Team.Enemy)
                {
                    targetUnit = unit;
                    surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(targetUnit.GetGameTile(), m_range, 1);
                    targetUnit.GetHitBySpell(GetSpellValue(), this);
                    break;
                }
            }
        }

        GameHelper.GetGameController().RemoveIntermissionLock();
    }
}