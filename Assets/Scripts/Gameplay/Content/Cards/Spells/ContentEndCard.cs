using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentEndCard : GameCardSpellBase
{
    private int m_range = 5;

    public ContentEndCard()
    {
        m_spellEffect = 10;

        m_name = "Burning Storm";
        m_targetType = Target.Enemy;
        m_cost = 5;
        m_rarity = GameRarity.Uncommon;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.MagicPower);
        m_tags.AddTag(GameTag.TagType.HighCost);

        m_audioCategory = AudioHelper.SpellAudioCategory.Damage;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return "Deal " + m_spellEffect + mpString + " damage to target enemy and all enemies in range " + m_range;
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(targetUnit.GetGameTile(), m_range, 1);

        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            GameUnit unit = surroundingTiles[i].GetOccupyingUnit();

            if (unit != null && !unit.m_isDead && unit.GetTeam() == Team.Enemy)
            {
                unit.GetHitBySpell(GetSpellValue(), this);
            }
        }

        targetUnit.GetHitBySpell(GetSpellValue(), this);
    }
}