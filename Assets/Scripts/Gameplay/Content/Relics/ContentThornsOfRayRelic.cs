using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentThornsOfRayRelic : GameRelic
{
    public ContentThornsOfRayRelic()
    {
        m_name = "Thorns Of Ray";
        m_desc = "Allied units have <b>Thorns 2</b>.";
        m_rarity = GameRarity.Common;

        LateInit();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Tank);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Healing);
    }
}
