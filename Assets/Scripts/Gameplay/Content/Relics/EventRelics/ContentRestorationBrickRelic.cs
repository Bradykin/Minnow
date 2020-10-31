using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRestorationBrickRelic : GameRelic
{
    public ContentRestorationBrickRelic()
    {
        m_name = "Restoration Brick";
        m_desc = "Buildings cost 15 less gold.";
        m_rarity = GameRarity.Event;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Gold);
    }
}
