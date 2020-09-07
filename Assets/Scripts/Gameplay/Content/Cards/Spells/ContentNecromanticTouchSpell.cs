using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentNecromanticTouchSpell : GameCardSpellBase
{
    private int m_range;

    public ContentNecromanticTouchSpell()
    {
        m_range = 2;

        m_spellEffect = 4;

        m_name = "Necromantic Touch";
        m_desc = "Deal " + GetSpellValue() + " damage to a target, heal all friendly entities within range " + m_range + " for the same ammount.";
        m_playDesc = "Zaaaaam!";
        m_targetType = Target.Entity;
        m_cost = 1;
        m_rarity = GameRarity.Common;

        SetupBasicData();
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        targetEntity.GetHit(GetSpellValue());

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingTiles(targetEntity.m_curTile, m_range, 1);

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
