using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentJoltCard : GameCardSpellBase
{
    private int m_mapUnlockID = 2;
    private int m_rankZeroChaosLevel = 1;
    private int m_rankOneChaosLevel = 4;
    private int m_rankTwoChaosLevel = 7;
    private int m_rankThreeChaosLevel = 10;

    public ContentJoltCard()
    {
        m_name = "Jolt";
        m_targetType = Target.Ally;
        m_rarity = GameRarity.Starter;

        SetCardLevel(GetCardLevel());

        SetupBasicData();
    }

    public override string GetDesc()
    {
        string description = "Target allied unit gains +" + m_spellEffect + " Stamina.";

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

        targetUnit.GainStamina(m_spellEffect);

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
        m_spellEffect = 2;

        if (m_cardLevel >= 1)
        {
            m_spellEffect = 2;
        }

        if (m_cardLevel >= 2)
        {
            m_cost = 0;
        }
    }
}
