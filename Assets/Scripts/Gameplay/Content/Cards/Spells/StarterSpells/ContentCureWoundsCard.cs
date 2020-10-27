using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCureWoundsCard : GameCardSpellBase
{
    public ContentCureWoundsCard()
    {
        m_name = "Cure Wounds";
        m_targetType = Target.Ally;
        m_rarity = GameRarity.Starter;

        InitializeWithLevel(GetCardLevel());

        SetupBasicData();
    }

    public override string GetDesc()
    {
        string description = GetHealDescString();

        if (GetCardLevel() >= 2)
        {
            description += "\nTrigger <b>Enrage</b> on the target.\n";
        }

        int numTraditionalMethods = GameHelper.RelicCount<ContentTraditionalMethodsRelic>();

        if (numTraditionalMethods > 1)
        {
            description += "\nDraw " + numTraditionalMethods + " cards.";
        }
        else if (numTraditionalMethods > 0)
        {
            description += "\nDraw a card.";
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

        targetUnit.Heal(GetSpellValue());

        if (GetCardLevel() >= 2)
        {
            List<GameEnrageKeyword> enrageKeywords = targetUnit.GetKeywords<GameEnrageKeyword>();
            int numBestialWrath = GameHelper.RelicCount<ContentBestialWrathRelic>();

            for (int i = 0; i < enrageKeywords.Count; i++)
            {
                enrageKeywords[i].DoAction(0);
                for (int k = 0; k < numBestialWrath; k++)
                {
                    enrageKeywords[i].DoAction(0);
                }
            }
        }

        int numTraditionalMethods = GameHelper.RelicCount<ContentTraditionalMethodsRelic>();
        for (int i = 0; i < numTraditionalMethods; i++)
        {
            GameHelper.GetPlayer().DrawCard();
        }
    }

    public override void InitializeWithLevel(int level)
    {
        m_cost = 1;
        m_spellEffect = 8;

        if (level >= 1)
        {
            m_spellEffect = 20;
        }

        if (level >= 2)
        {
            //Triggers enrage on the unit
        }
    }
}
