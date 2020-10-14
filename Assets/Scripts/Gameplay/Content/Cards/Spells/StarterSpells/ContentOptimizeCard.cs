﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentOptimizeCard : GameCardSpellBase
{
    private int m_mapUnlockID = 2;
    private int m_rankZeroChaosLevel = 1;
    private int m_rankOneChaosLevel = 4;
    private int m_rankTwoChaosLevel = 7;
    private int m_rankThreeChaosLevel = 10;

    private int m_cardDraw = 1;
    private int m_energyGain = 1;

    public ContentOptimizeCard()
    {
        m_name = "Optimize";
        m_targetType = Target.Ally;
        m_rarity = GameRarity.Starter;
        m_shouldExile = true;

        SetCardLevel(GetCardLevel());

        SetupBasicData();
    }

    public override string GetDesc()
    {
        return "Gain " + m_energyGain + " Energy and draw " + m_cardDraw + " cards.";
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

        GamePlayer player = GameHelper.GetPlayer();

        player.AddEnergy(m_energyGain);
        player.DrawCards(m_cardDraw);
    }

    public override void SetCardLevel(int level)
    {
        base.SetCardLevel(level);

        m_cost = 1;

        if (m_cardLevel >= 1)
        {
            m_cardDraw = 2;
            m_energyGain = 2;
        }

        if (m_cardLevel >= 2)
        {
            m_cost = 0;
        }
    }
}
