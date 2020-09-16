using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCureWoundsCard : GameCardSpellBase
{
    public ContentCureWoundsCard()
    {
        m_spellEffect = 8;

        m_name = "Cure Wounds";
        m_playDesc = "A stream of healing restores the troops!";
        m_targetType = Target.Ally;
        m_cost = 2;
        m_rarity = GameRarity.Starter;

        SetupBasicData();
    }

    public override string GetDesc()
    {
        return GetHealDescString() + "\nDraw a card.";
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);
        GameHelper.GetPlayer().DrawCard();

        targetEntity.Heal(GetSpellValue());
    }
}
