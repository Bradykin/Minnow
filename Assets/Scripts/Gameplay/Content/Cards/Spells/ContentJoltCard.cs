using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentJoltCard : GameCardSpellBase
{
    public ContentJoltCard()
    {
        m_name = "Jolt";
        m_desc = "Restore 1 AP.\nDraw a card.";
        m_playDesc = "The target gets a jolt of energy!";
        m_targetType = Target.Ally;
        m_cost = 0;
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

        targetEntity.GainAP(1);

        GameHelper.GetPlayer().DrawCard();
    }
}
