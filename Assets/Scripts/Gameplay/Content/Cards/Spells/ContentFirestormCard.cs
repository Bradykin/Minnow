using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFirestormCard : GameCardSpellBase
{
    private int m_numHits = 3;
    
    public ContentFirestormCard()
    {
        m_spellEffect = 1;

        m_name = "Firestorm";
        m_targetType = Target.Unit;
        m_cost = 1;
        m_rarity = GameRarity.Common;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.DamageSpell);
        m_tags.AddTag(GameTag.TagType.Spellpower);
        m_tags.AddTag(GameTag.TagType.Enrage);
    }

    public override string GetDesc()
    {
        string description = GetDamageDescString();

        description += "Do this " + m_numHits + " times.";

        return description;
    }

    public override void PlayCard(GameUnit targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        for (int i = 0; i < m_numHits; i++)
        {
            if (targetEntity.m_isDead)
            {
                break;
            }
            targetEntity.GetHit(GetSpellValue());
        }
    }
}
