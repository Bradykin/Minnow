using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDwarfShivcasterCard : GameUnitCard
{
    public ContentDwarfShivcasterCard()
    {
        m_unit = new ContentDwarfShivcaster();

        m_cost = 2;
        m_playerUnlockLevel = 4;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.MagicPower);
        m_tags.AddTag(GameTag.TagType.Shiv);
        m_tags.AddTag(GameTag.TagType.Midrange);
    }
}
