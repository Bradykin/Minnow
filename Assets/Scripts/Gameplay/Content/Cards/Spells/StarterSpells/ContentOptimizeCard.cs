using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentOptimizeCard : GameCardSpellBase
{
    private int m_cardDraw = 2;
    private int m_energyGain = 3;

    public ContentOptimizeCard()
    {
        m_name = "Optimize";
        m_targetType = Target.Ally;
        m_cost = 1;
        m_rarity = GameRarity.Starter;
        m_shouldExile = true;

        SetupBasicData();
    }

    public override string GetDesc()
    {
        return "Gain " + m_energyGain + " Energy and draw " + m_cardDraw + " cards.";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        GamePlayer player = GameHelper.GetPlayer();

        player.AddEnergy(m_energyGain);
        player.DrawCards(m_cardDraw);
    }
}
