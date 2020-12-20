using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTokenOfFriendshipRelic : GameRelic
{
    public ContentTokenOfFriendshipRelic()
    {
        m_name = "Token of Friendship";
        m_desc = "Allied <b>Humanoid</b> units have <b>Mountainwalk</b>.";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tagHolder.AddTag(GameTagHolder.TagType.Mountain);
        m_tagHolder.AddTag(GameTagHolder.TagType.Humanoid);
    }
}
