﻿using System.Collections;
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
        m_targetType = Target.Enemy;
        m_cost = 2;
        m_rarity = GameRarity.Common;

        SetupBasicData();

        m_tagHolder.AddPullTag(GameTagHolder.TagType.Healing);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.DamageSpell);

        m_onPlaySFX = AudioHelper.NecromanticTouch;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return $"{GetDamageDescString()}Heal all allied units within range {m_range} for {UIHelper.GetMagicPowerColoredValue(m_spellEffect + mpString)}.";
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

        targetUnit.GetHitBySpell(GetSpellValue(), this);
    }
}
