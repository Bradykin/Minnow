using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWildwoodExplorerCard : GameUnitCard
{
    public ContentWildwoodExplorerCard()
    {
        m_unit = new ContentWildwoodExplorer();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Midrange);
        m_tags.AddTag(GameTag.TagType.Forest);
    }
}
