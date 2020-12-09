using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMoonbeamCard : GameCardSpellBase
{
    private int m_range = 3;

    public ContentMoonbeamCard()
    {
        m_spellEffect = 70;

        m_name = "Moonbeam";
        m_targetType = Target.None;
        m_cost = 1;
        m_rarity = GameRarity.Rare;
        m_shouldExile = true;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.DamageSpell);

        m_audioCategory = AudioHelper.SpellAudioCategory.Damage;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return "Deal " + m_spellEffect + mpString + " damage to all enemies in range " + m_range + " of your castle!";
    }

    public override void PlayCard()
    {
        if (!IsValidToPlay())
        {
            return;
        }

        GameHelper.GetGameController().AddIntermissionLock();

        base.PlayCard();

        GameTile castleTile = GameHelper.GetPlayer().GetCastleGameTile();

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(castleTile, m_range, 1);

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