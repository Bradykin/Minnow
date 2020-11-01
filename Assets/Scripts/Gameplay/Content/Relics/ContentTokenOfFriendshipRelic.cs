using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTokenOfFriendshipRelic : GameRelic
{
    public ContentTokenOfFriendshipRelic()
    {
        m_name = "Token of Friendship";
        m_desc = "Allied Humanoids gain Mountainwalk when summoned.";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Mountain);
        m_tags.AddTag(GameTag.TagType.Humanoid);
    }
}
