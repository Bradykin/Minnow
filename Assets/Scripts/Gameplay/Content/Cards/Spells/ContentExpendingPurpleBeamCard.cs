using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentExpendingPurpleBeamCard : GameCardSpellBase
{
    public ContentExpendingPurpleBeamCard()
    {
        m_spellEffect = 6;

        m_name = "Expending Purple Beam";
        m_playDesc = "Feel the purple!";
        m_targetType = Target.Entity;
        m_cost = 1;
        m_rarity = GameRarity.Common;

        SetupBasicData();
    }

    public override string GetDesc()
    {
        return "Spend up to 5 from the purple beam count to deal damage equal to triple the amount of purple beams expended.";
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        int numToSpend = Mathf.Min(Globals.m_purpleBeamCount, 5);

        targetEntity.GetHit(numToSpend * 3);
        Globals.m_purpleBeamCount -= numToSpend;
    }
}
