using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFireboltCard : GameCardSpellBase
{
    public ContentFireboltCard()
    {
        m_spellEffect = 4;

        m_name = "Firebolt";
        m_playDesc = "The foe is blasted!";
        m_targetType = Target.Entity;
        m_cost = 1;
        m_rarity = GameRarity.Starter;

        SetupBasicData();
    }

    public override string GetDesc()
    {
        return GetDamageDescString();
    }

    protected override int GetSpellValue()
    {
        int spellValue = base.GetSpellValue();

        spellValue += 2 * GameHelper.RelicCount<ContentTraditionalMethodsRelic>();

        return spellValue;
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
