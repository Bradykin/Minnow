using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCureWoundsCard : GameCardSpellBase
{
    public ContentCureWoundsCard()
    {
        m_spellEffect = 12;

        m_name = "Cure Wounds";
        m_playDesc = "A stream of healing restores the troops!";
        m_targetType = Target.Ally;
        m_cost = 1;
        m_rarity = GameRarity.Common;

        SetupBasicData();
    }

    public override string GetDesc()
    {
        return GetHealDescString();
    }

    protected override int GetSpellValue()
    {
        int spellValue = base.GetSpellValue();

        spellValue += 3 * GameHelper.RelicCount<ContentTraditionalMethodsRelic>();

        return spellValue;
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
