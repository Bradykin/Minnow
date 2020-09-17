using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFletchingCard : GameCardSpellBase
{
    public ContentFletchingCard()
    {
        m_name = "Fletching";
        m_desc = "Ally ranged units get +2 power until end of turn.";
        m_targetType = Target.None;
        m_cost = 1;
        m_rarity = GameRarity.Common;

        SetupBasicData();
    }

    public override void PlayCard()
    {
        if (!IsValidToPlay())
        {
            return;
        }

        base.PlayCard();

        Globals.m_fletchingCount += 2;
    }
}
