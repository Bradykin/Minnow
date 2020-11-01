using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentEyeOfTelloRelic : GameRelic
{
    public ContentEyeOfTelloRelic()
    {
        m_name = "Eye of Tello";
        m_desc = "You can look at and play the top card of your deck.  However, you can no longer look at your current deck.";
        m_rarity = GameRarity.Rare;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Knowledgeable);
    }
}
