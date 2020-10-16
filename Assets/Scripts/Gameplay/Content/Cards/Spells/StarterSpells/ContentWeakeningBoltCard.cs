using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWeakeningBoltCard : GameCardSpellBase
{
    private int m_powerToDrain = 2;

    public ContentWeakeningBoltCard()
    {
        m_name = "Weakening Bolt";
        m_targetType = Target.Unit;
        m_rarity = GameRarity.Starter;

        SetCardLevel(GetCardLevel());

        SetupBasicData();
    }

    public override string GetDesc()
    {
        string description = GetDamageDescString() + "Drain " + m_powerToDrain + " Power.\n";

        int numTraditionalMethods = GameHelper.RelicCount<ContentTraditionalMethodsRelic>();

        if (numTraditionalMethods > 1)
        {
            description += "Draw " + numTraditionalMethods + " cards.";
        }
        else if (numTraditionalMethods > 0)
        {
            description += "Draw a card.";
        }

        return description;
    }

    public override bool PlayerHasUnlockedCard()
    {
        return Constants.CheatsOn || (base.PlayerHasUnlockedCard() && GameMetaProgression.IsChaosLevelAchieved(m_mapUnlockID, 1));
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.GetHit(GetSpellValue());

        int numTraditionalMethods = GameHelper.RelicCount<ContentTraditionalMethodsRelic>();
        for (int i = 0; i < numTraditionalMethods; i++)
        {
            GameHelper.GetPlayer().DrawCard();
        }

        targetUnit.RemovePower(m_powerToDrain);
    }

    public override void SetCardLevel(int level)
    {
        base.SetCardLevel(level);

        m_cost = 1;
        m_spellEffect = 2;

        if (m_cardLevel >= 1)
        {
            m_spellEffect = 4;
        }

        if (m_cardLevel >= 2)
        {
            m_powerToDrain = 5;
        }
    }
}
