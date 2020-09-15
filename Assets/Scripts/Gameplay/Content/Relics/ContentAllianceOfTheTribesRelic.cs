using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentAllianceOfTheTribesRelic : GameRelic
{
    public ContentAllianceOfTheTribesRelic()
    {
        m_name = "Alliance Of The Tribes";
        m_desc = "If you have at least one unit of each type in play, all ally units get +2 ap regen.";
        m_rarity = GameRarity.Rare;

        LateInit();
    }
}
