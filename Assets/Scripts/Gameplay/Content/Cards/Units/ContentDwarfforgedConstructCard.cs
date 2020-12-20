using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDwarfforgedConstructCard : GameUnitCard
{
    public ContentDwarfforgedConstructCard()
    {
        m_unit = new ContentDwarfforgedConstruct();

        m_cost = 3;

        FillBasicData();

        m_tagHolder.AddPullTag(GameTagHolder.TagType.Tank);
    }
}
