using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBurningStormCard : GameCardSpellBase
{
    private int m_range = 2;

    public ContentBurningStormCard()
    {
        m_spellEffect = 10;

        m_name = "Burning Storm";
        m_targetType = Target.Enemy;
        m_cost = 2;
        m_rarity = GameRarity.Common;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.MagicPower);

        m_onPlaySFX = AudioHelper.FireBlast;
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

        GameHelper.GetGameController().AddIntermissionLock();
        base.PlayCard(targetUnit);

        GameTile centerTile = targetUnit.GetGameTile();

        targetUnit.GetHitBySpell(GetSpellValue(), this);

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(centerTile, m_range, 1);

        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            GameUnit unit = surroundingTiles[i].GetOccupyingUnit();

            if (unit != null && !unit.m_isDead && unit.GetTeam() == Team.Enemy)
            {
                unit.GetHitBySpell(GetSpellValue(), this);
            }
        }

        GameHelper.GetGameController().RemoveIntermissionLock();
    }
}