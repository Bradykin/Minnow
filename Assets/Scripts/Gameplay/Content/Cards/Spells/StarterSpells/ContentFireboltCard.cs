using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFireboltCard : GameCardSpellBase
{
    public ContentFireboltCard()
    {
        m_name = "Firebolt";
        m_targetType = Target.Unit;
        m_rarity = GameRarity.Starter;

        SetCardLevel(GetCardLevel());

        SetupBasicData();
    }

    public override string GetDesc()
    {
        string description = GetDamageDescString();

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
    }

    public override void SetCardLevel(int level)
    {
        base.SetCardLevel(level);

        m_cost = 1;
        m_spellEffect = 5;

        if (m_cardLevel >= 1)
        {
            m_spellEffect += 4;
        }
        
        if (m_cardLevel >= 2)
        {
            m_cost = 0;
        }
    }
}
