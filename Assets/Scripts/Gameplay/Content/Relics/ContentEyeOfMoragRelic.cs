using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentEyeOfMoragRelic : GameRelic
{
    public ContentEyeOfMoragRelic()
    {
        m_name = "Eye of Morag";
        m_desc = "When you gain this, all forests on the map burn.  All units on them die.";
        m_rarity = GameRarity.Rare;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Forest);
    }
}
