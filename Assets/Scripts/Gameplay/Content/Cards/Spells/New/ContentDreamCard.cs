using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDreamCard : GameCardSpellBase
{
    private int m_toDraw = 3;

    public ContentDreamCard()
    {
        m_name = "Dream";
        m_desc = "Draw " + m_toDraw + " cards.";
        m_playDesc = "You draw some cards!";
        m_targetType = Target.None;
        m_typeline = "Spell - " + m_targetType;
        m_cost = 1;
        m_icon = UIHelper.GetIconCard(m_name);
        m_rarity = GameRarity.Uncommon;
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

        player.DrawCards(m_toDraw);
    }
}