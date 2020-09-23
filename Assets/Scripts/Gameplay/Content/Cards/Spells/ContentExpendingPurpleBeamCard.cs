using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentExpendingPurpleBeamCard : GameCardSpellBase
{
    private int m_multiplier = 10;

    public ContentExpendingPurpleBeamCard()
    {
        m_spellEffect = 5;

        m_name = "Expending Purple Beam";
        m_playDesc = "Feel the purple!";
        m_targetType = Target.Entity;
        m_cost = 1;
        m_rarity = GameRarity.Common;

        SetupBasicData();
    }

    public override string GetDesc()
    {
        return "Spend up to " + m_spellEffect + " (" + GetSpellValue() + ") from the purple beam count to deal damage equal to " + m_multiplier  + " * the amount of purple beams expended.\n" + GetModifiedBySpellPowerString();
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        int numToSpend = Mathf.Min(Globals.m_purpleBeamCount, GetSpellValue());

        targetEntity.GetHit(numToSpend * m_multiplier);
        Globals.m_purpleBeamCount -= numToSpend;
    }
}
