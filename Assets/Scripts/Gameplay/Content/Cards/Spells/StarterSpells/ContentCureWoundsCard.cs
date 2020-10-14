using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCureWoundsCard : GameCardSpellBase
{
    private int m_mapUnlockID = 1;
    private int m_rankZeroChaosLevel = 1;
    private int m_rankOneChaosLevel = 4;
    private int m_rankTwoChaosLevel = 7;
    private int m_rankThreeChaosLevel = 10;

    public ContentCureWoundsCard()
    {
        m_name = "Cure Wounds";
        m_targetType = Target.Ally;
        m_rarity = GameRarity.Starter;

        SetCardLevel(GetCardLevel());

        SetupBasicData();
    }

    public override string GetDesc()
    {
        string description = GetHealDescString();

        if (m_cardLevel >= 2)
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

    public override bool PlayerHasUnlockedCard()
    {
        return Constants.CheatsOn || (base.PlayerHasUnlockedCard() && GameMetaProgression.IsChaosLevelAchieved(m_mapUnlockID, m_rankZeroChaosLevel));
    }

    public int GetCardLevel()
    {
        if (!GameMetaProgression.IsMapUnlocked(m_mapUnlockID))
        {
            return 0;
        }

        if (GameMetaProgression.IsChaosLevelAchieved(m_mapUnlockID, m_rankThreeChaosLevel))
        {
            return 3;
        }

        if (GameMetaProgression.IsChaosLevelAchieved(m_mapUnlockID, m_rankTwoChaosLevel))
        {
            return 2;
        }

        if (GameMetaProgression.IsChaosLevelAchieved(m_mapUnlockID, m_rankOneChaosLevel))
        {
            return 1;
        }

        return 0;
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.Heal(GetSpellValue());

        if (m_cardLevel >= 2)
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

    public override void SetCardLevel(int level)
    {
        base.SetCardLevel(level);

        m_cost = 1;
        m_spellEffect = 8;

        if (m_cardLevel >= 1)
        {
            m_spellEffect = 20;
        }

        if (m_cardLevel >= 2)
        {
            //Triggers enrage on the unit
        }
    }
}
