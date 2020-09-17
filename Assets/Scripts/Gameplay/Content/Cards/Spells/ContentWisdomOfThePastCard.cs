﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWisdomOfThePastCard : GameCardSpellBase
{
    public ContentWisdomOfThePastCard()
    {
        m_name = "Wisdom Of The Past";
        m_targetType = Target.None;
        m_cost = 1;
        m_rarity = GameRarity.Uncommon;

        SetupBasicData();
    }

    public override string GetDesc()
    {
        return "Draw cards equal to the number of spells played last turn (" + Globals.m_spellsPlayedPreviousTurn + ").";
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
