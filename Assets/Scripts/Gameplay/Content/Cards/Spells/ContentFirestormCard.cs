using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFirestormCard : GameCardSpellBase
{
    private int m_numHits = 5;
    
    public ContentFirestormCard()
    {
        m_spellEffect = 1;

        m_name = "Firestorm";
        m_targetType = Target.Unit;
        m_cost = 1;
        m_rarity = GameRarity.Common;

        SetupBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.MagicPower);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Enrage);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.DamageSpell);

        m_onPlaySFX = AudioHelper.FireBlast;
    }

    public override string GetDesc()
    {
        string description = GetDamageDescString();

        description += "Do this " + m_numHits + " times.";

        return description;
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        for (int i = 0; i < m_numHits; i++)
        {
            if (targetUnit.m_isDead)
            {
                break;
            }
            targetUnit.GetHitBySpell(GetSpellValue(), this);
        }
    }
}
