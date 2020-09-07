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
        m_cost = 1;
        m_rarity = GameRarity.Common;

        SetupBasicData();
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