using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentNightWingsCard : GameCardSpellBase
{
    public ContentNightWingsCard()
    {
        m_name = "Night Wings";
        m_desc = "Give an entity flying, but it loses 1 AP regen.";
        m_playDesc = "The grows scary wings!  Boo!";
        m_targetType = Target.Ally;
        m_cost = 3;
        m_rarity = GameRarity.Rare;
        m_shouldExile = true;

        SetupBasicData();
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        targetEntity.AddKeyword(new GameFlyingKeyword());
        targetEntity.AddAPRegen(-1);
    }
}