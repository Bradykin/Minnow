using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentAegisCard : GameCardSpellBase
{
    private int m_amount = 1;
    
    public ContentAegisCard()
    {
        m_name = "Aegis";
        m_desc = "Give target ally " + m_amount + " damage shield.";
        m_playDesc = "Shields up!";
        m_targetType = Target.Ally;
        m_cost = 1;
        m_rarity = GameRarity.Starter;

        SetupBasicData();
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        GameDamageShieldKeyword damageShieldKeyword = targetEntity.GetKeyword<GameDamageShieldKeyword>();

        if (damageShieldKeyword == null)
        {
            targetEntity.AddKeyword(new GameDamageShieldKeyword(m_amount));
        }
        else
        {
            damageShieldKeyword.IncreaseShield(m_amount);
        }
    }
}
