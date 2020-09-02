using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentInsightCard : GameCardSpellBase
{
    private int m_spellcraftNum = 5;

    public ContentInsightCard()
    {
        m_name = "Insight";
        m_desc = "Trigger spellcraft " + m_spellcraftNum + " times (including this).";
        for (int i = 0; i < m_spellcraftNum; i++)
        {
            m_playDesc += "Spellcraft. ";
        }
        m_targetType = Target.None;
        m_typeline = "Spell - " + m_targetType;
        m_cost = 1;
        m_icon = UIHelper.GetIconCard(m_name);
        m_rarity = GameRarity.Rare;
    }

    public override void PlayCard()
    {
        if (!IsValidToPlay())
        {
            return;
        }

        base.PlayCard();

        GamePlayer player = GameHelper.GetPlayer();
        if (player == null)
        {
            return;
        }

        for (int i = 0; i < m_spellcraftNum-1; i++)
        {
            TriggerSpellcraft();
        }
    }
}