using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentOptimizeCard : GameCardSpellBase
{
    private int m_cardDraw = 3;
    private int m_energyGain = 3;

    public ContentOptimizeCard()
    {
        m_name = "Optimize";
        m_targetType = Target.None;
        m_rarity = GameRarity.Starter;
        m_shouldExile = true;

        m_cost = 1;

        SetupBasicData();

        m_onPlaySFX = AudioHelper.MiscEffect;
    }

    public override string GetDesc()
    {
         return "Gain " + m_energyGain + " energy and draw " + m_cardDraw + " cards.";
    }

    public override void PlayCard()
    {
        if (!IsValidToPlay())
        {
            return;
        }

        base.PlayCard();

        GamePlayer player = GameHelper.GetPlayer();

        player.AddEnergy(m_energyGain);
        player.DrawCards(m_cardDraw);
    }
}
