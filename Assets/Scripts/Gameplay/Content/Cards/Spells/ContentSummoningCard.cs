using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSummoningCard : GameCardSpellBase
{
    public ContentSummoningCard()
    {
        m_name = "Summoning";
        m_desc = "Add a random unit card to your hand.";
        m_targetType = Target.None;
        m_cost = 0;
        m_rarity = GameRarity.Rare;
        m_shouldExile = true;

        SetupBasicData();

        m_onPlaySFX = AudioHelper.MagicEffect;
    }

    public override void PlayCard()
    {
        if (!IsValidToPlay())
        {
            return;
        }

        base.PlayCard();

        GameHelper.GetPlayer().AddCardToHand(GameCardFactory.GetRandomStandardUnitCard(), false);
    }
}
