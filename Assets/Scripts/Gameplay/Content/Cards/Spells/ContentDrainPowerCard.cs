using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDrainPowerCard : GameCardSpellBase
{
    private int m_range = 2;
    private int m_powerGain = 3;

    public ContentDrainPowerCard()
    {
        m_spellEffect = 4;

        m_name = "Drain Power";
        m_targetType = Target.Enemy;
        m_cost = 2;
        m_rarity = GameRarity.Common;

        SetupBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.MagicPower);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.DamageSpell);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.BuffSpell);

        m_onPlaySFX = AudioHelper.MagicBolt;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return "Deal " + m_spellEffect + mpString + " damage to target enemy. If it dies, all allied units in range " + m_range + " get +" + m_powerGain + "/+0.";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(targetUnit.GetGameTile(), m_range, 1);

        targetUnit.GetHitBySpell(GetSpellValue(), this);

        if (targetUnit.m_isDead)
        {
            for (int i = 0; i < surroundingTiles.Count; i++)
            {
                GameUnit unit = surroundingTiles[i].GetOccupyingUnit();

                if (unit != null && !unit.m_isDead && unit.GetTeam() == Team.Player)
                {
                    unit.AddStats(m_powerGain, 0, false, true);
                }
            }
        }
    }
}