using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPirateCaptainCard : GameUnitCard
{
    public ContentPirateCaptainCard()
    {
        m_unit = new ContentPirateCaptain();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Water);
        m_tags.AddTag(GameTag.TagType.Explorer);
    }
}
