using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentNecromanticTouchCard : GameCardSpellBase
{
    private int m_range;

    public ContentNecromanticTouchCard()
    {
        m_range = 2;

        m_spellEffect = 14;

        m_name = "Necromantic Touch";
        m_targetType = Target.Unit;
        m_cost = 2;
        m_rarity = GameRarity.Common;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.DamageSpell);
        m_tags.AddTag(GameTag.TagType.Healing);

        m_audioCategory = AudioHelper.SpellAudioCategory.Damage;
    }

    public override string GetDesc()
    {
        return GetDamageDescString() + "Heal all allied units within range " + m_range + " for the same amount.";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.GetHit(GetSpellValue());

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(targetUnit.GetGameTile(), m_range, 1);

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

            unit.Heal(GetSpellValue());
        }
    }
}
