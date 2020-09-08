﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPurpleBeamSwordCard : GameCardSpellBase
{
    public ContentPurpleBeamSwordCard()
    {
        m_name = "Purple Beam Sword";
        m_desc = "Give an entity Victorious: Gain a purple beam count.";
        m_playDesc = "Zwooo-PURPLE-om!";
        m_targetType = Target.Entity;
        m_cost = 3;
        m_rarity = GameRarity.Uncommon;

        SetupBasicData();
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        targetEntity.GetKeywordHolder().m_keywords.Add(new GameVictoriousKeyword(new GainPurpleBeamAction(1)));
    }
}

public class GainPurpleBeamAction : GameAction
{
    private int m_toGain;

    public GainPurpleBeamAction(int toGain)
    {
        m_toGain = toGain;

        m_name = "Gain Purple Beam";
        m_desc = "+ " + m_toGain + " purple beam count.";
    }

    public override void DoAction()
    {
        Globals.m_purpleBeamCount += m_toGain;
    }
}
