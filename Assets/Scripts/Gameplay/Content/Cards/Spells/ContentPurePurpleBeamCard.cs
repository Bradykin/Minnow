using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPurePurpleBeamCard : GameCardSpellBase
{
    public ContentPurePurpleBeamCard()
    {
        m_name = "Pure Purple Beam";
        m_desc = "If the target has AP, drain it and gain that much purple beam count.  Otherwise, drain your purple beam to restore it's AP.";
        m_playDesc = "Drain purple drain!";
        m_targetType = Target.Entity;
        m_cost = 2;
        m_rarity = GameRarity.Rare;

        SetupBasicData();
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        if (targetEntity.GetCurAP() == 0)
        {
            int toDrain = Mathf.Min(Globals.m_purpleBeamCount, targetEntity.GetMaxAP());
            targetEntity.GainAP(toDrain);
            Globals.m_purpleBeamCount -= toDrain;
        }
        else
        {
            Globals.m_purpleBeamCount += targetEntity.GetCurAP();
            targetEntity.SpendAP(targetEntity.GetMaxAP());
        }
    }
}