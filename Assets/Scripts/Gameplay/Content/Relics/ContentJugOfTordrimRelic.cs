using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentJugOfTordrimRelic : GameRelic
{
    public ContentJugOfTordrimRelic()
    {
        m_name = "Jug of Tordrim";
        m_desc = "Whenever a unit is summoned, swap it's power and health.";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Tordrim);
        m_tags.AddTag(GameTag.TagType.BuffSpell);
        //TODO: Alex - Once you can split tags out from being always push/pull, this should have a pull Scaler tag
    }
}
