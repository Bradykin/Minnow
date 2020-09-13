using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSkypierceCard : GameCardSpellBase
{
    public ContentSkypierceCard()
    {
        m_spellEffect = 5;

        m_name = "Skypierce";
        m_playDesc = "BOOM!";
        m_targetType = Target.Enemy;
        m_cost = 1;
        m_rarity = GameRarity.Common;

        SetupBasicData();
    }

    public override string GetDesc()
    {
        return GetDamageDescString() + "Instantly kill if it has flying (and is not an elite or boss).";
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        if (targetEntity.GetKeyword<GameFlyingKeyword>() != null && !GameHelper.IsBossOrElite(targetEntity))
        {
            targetEntity.Die();
        }
        else
        {
            targetEntity.GetHit(GetSpellValue());
        }
    }
}
