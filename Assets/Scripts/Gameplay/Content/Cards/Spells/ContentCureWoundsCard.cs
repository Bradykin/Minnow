using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCureWoundsCard : GameCardSpellBase
{
    public ContentCureWoundsCard()
    {
        m_spellEffect = 5;

        m_name = "Cure Wounds";
        m_desc = "Heal a freindly entity for " + GetSpellValue() + " health.";
        m_playDesc = "A stream of healing heals for " + GetSpellValue();
        m_targetType = Target.Ally;
        m_cost = 1;
        m_rarity = GameRarity.Starter;

        SetupBasicData();
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        targetEntity.Heal(GetSpellValue());
    }
}
