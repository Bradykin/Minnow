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

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Forest);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.Explorer);
    }
}
