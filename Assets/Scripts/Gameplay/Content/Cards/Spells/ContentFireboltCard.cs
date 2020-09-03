using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFireboltCard : GameCardSpellBase
{
    public ContentFireboltCard()
    {
        m_spellEffect = 3;

        m_name = "Firebolt";
        m_desc = "Blast an enemy for " + GetSpellValue() + " damage.";
        m_playDesc = "A bolt of fire strikes the foe for " + GetSpellValue();
        m_targetType = Target.Entity;
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

        targetEntity.GetHit(GetSpellValue());
    }
}
