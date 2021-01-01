using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentEndCard : GameCardSpellBase
{
    private int m_range = 5;

    public ContentEndCard()
    {
        m_spellEffect = 40;

        m_name = "End";
        m_targetType = Target.Enemy;
        m_cost = 5;
        m_rarity = GameRarity.Uncommon;

        SetupBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.MagicPower);

        m_onPlaySFX = AudioHelper.LargeImpact;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return $"Deal {UIHelper.GetMagicPowerColoredValue(m_spellEffect + mpString)} damage to target enemy and all enemies in range {m_range}";
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

        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            GameUnit unit = surroundingTiles[i].GetOccupyingUnit();

            if (unit != null && !unit.m_isDead && unit.GetTeam() == Team.Enemy)
            {
                unit.GetHitBySpell(GetSpellValue(), this);
            }
        }

        targetUnit.GetHitBySpell(GetSpellValue(), this);

        GameHelper.GetGameController().RemoveIntermissionLock();
    }
}