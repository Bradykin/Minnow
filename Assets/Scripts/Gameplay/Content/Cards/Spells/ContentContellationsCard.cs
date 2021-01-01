using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentContellationsCard : GameCardSpellBase
{
    private int m_statBuff = 5;

    public ContentContellationsCard()
    {
        m_name = "Constellations";
        m_targetType = Target.Ally;
        m_cost = 1;
        m_rarity = GameRarity.Common;
        m_shouldExile = true;
        m_spellEffect = 1;

        SetupBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.BuffSpell);

        m_onPlaySFX = AudioHelper.SmallBuff;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return $"Target allied unit gains +{m_statBuff}/+{m_statBuff} for each enemy in range {UIHelper.GetMagicPowerColoredValue(m_spellEffect + mpString)}.";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        GameTile castleTile = GameHelper.GetPlayer().GetCastleGameTile();

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(castleTile, GetSpellValue(), 1);

        int numEnemies = 0;
        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            GameUnit unit = surroundingTiles[i].GetOccupyingUnit();
            if (unit != null && !unit.m_isDead && unit.GetTeam() == Team.Enemy)
            {
                numEnemies++;
            }
        }

        targetUnit.AddStats(m_statBuff * numEnemies, m_statBuff * numEnemies, false, true);
    }
}
