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

        InitializeWithLevel(GetCardLevel());

        SetupBasicData();

        m_audioCategory = AudioHelper.SpellAudioCategory.Buff;
    }

    public override string GetDesc()
    {
        if (m_cardDraw == 1)
        {
            return "Gain " + m_energyGain + " energy and draw " + m_cardDraw + " card.";
        }
        else
        {
            return "Gain " + m_energyGain + " energy and draw " + m_cardDraw + " cards.";
        }
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

    public override void InitializeWithLevel(int level)
    {
        m_cost = 1;

        if (level >= 1)
        {
            m_cost = 0;
        }

        if (level >= 2)
        {
            m_cardDraw = 5;
            m_energyGain = 5;
        }
    }
}
