using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFortifyingPurpleBeamCard : GameCardSpellBase
{
    public ContentFortifyingPurpleBeamCard()
    {
        m_name = "Fortifying Purple Beam";
        m_desc = "Give an entity power and max health equal to your purple beam count.  Then drain your purple beam count.";
        m_playDesc = "PURPLE BEAM!";
        m_targetType = Target.Ally;
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

        targetEntity.AddPower(Globals.m_purpleBeamCount);
        targetEntity.AddMaxHealth(Globals.m_purpleBeamCount);

        Globals.m_purpleBeamCount = 0;
    }
}