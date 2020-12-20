using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMedKitRelic : GameRelic
{
    public ContentMedKitRelic()
    {
        m_name = "Med Kit";
        m_desc = "Allied <b>Humanoid</b> units heal equal to 5* the stamina cost of the tile they are on at the start of each round.";
        m_rarity = GameRarity.Common;

        LateInit();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Humanoid);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Forest);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.Healing);
    }
}
