using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDwarfShivcasterCard : GameUnitCard
{
    public ContentDwarfShivcasterCard()
    {
        m_unit = new ContentDwarfShivcaster();

        m_cost = 2;

        FillBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.MagicPower);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Shiv);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.Midrange);
    }
}
