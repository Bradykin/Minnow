using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSkypierceCard : GameCardSpellBase
{
    public ContentSkypierceCard()
    {
        m_spellEffect = 5;

        m_name = "Skypierce";
        m_desc = "Deal " + GetSpellValue() + " damage to a target.  Instantly kill if it has flying.";
        m_playDesc = "BOOM!";
        m_targetType = Target.Enemy;
        m_cost = 1;
        m_rarity = GameRarity.Common;

        SetupBasicData();
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        if (targetEntity.GetKeywordHolder().GetKeyword<GameFlyingKeyword>() != null)
        {
            targetEntity.Die();
        }
        else
        {
            targetEntity.GetHit(GetSpellValue());
        }
    }
}
