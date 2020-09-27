﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMechanizeCard : GameCardSpellBase
{
    public ContentMechanizeCard()
    {
        m_name = "Mechanize";
        m_desc = "Target ally creation loses all current AP and gains an equal amount of power.";
        m_playDesc = "Charging up!";
        m_targetType = Target.Ally;
        m_cost = 2;
        m_rarity = GameRarity.Common;

        SetupBasicData();
    }

    public override bool IsValidToPlay(GameEntity targetEntity)
    {
        return base.IsValidToPlay() && targetEntity.GetTypeline() == Typeline.Creation;
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        int curAP = targetEntity.GetCurAP();
        targetEntity.SpendAP(curAP);
        targetEntity.AddPower(curAP);
    }
}