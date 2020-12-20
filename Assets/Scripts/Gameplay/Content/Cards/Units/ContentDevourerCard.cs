using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDevourerCard : GameUnitCard
{
    public ContentDevourerCard()
    {
        m_unit = new ContentDevourer();

        m_cost = 2;

        FillBasicData();

        m_tagHolder.AddPullTag(GameTagHolder.TagType.Scaler);
    }
}
