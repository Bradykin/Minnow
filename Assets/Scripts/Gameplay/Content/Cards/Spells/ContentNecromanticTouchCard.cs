using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentNecromanticTouchCard : GameCardSpellBase
{
    private int m_range;

    public ContentNecromanticTouchCard()
    {
        m_range = 2;

        m_spellEffect = 12;

        m_name = "Necromantic Touch";
        m_targetType = Target.Unit;
        m_cost = 1;
        m_rarity = GameRarity.Uncommon;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.DamageSpell);
        m_tags.AddTag(GameTag.TagType.Healing);
    }

    public override string GetDesc()
    {
        return GetDamageDescString() + "Heal all allied units within range " + m_range + " for the same amount.";
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        targetEntity.GetHit(GetSpellValue());

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingTiles(targetEntity.GetGameTile(), m_range, 1);

        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            GameEntity entity = surroundingTiles[i].m_occupyingEntity;

            if (entity == null)
            {
                continue;
            }

            if (entity.GetTeam() != Team.Player)
            {
                continue;
            }

            entity.Heal(GetSpellValue());
        }
    }
}
