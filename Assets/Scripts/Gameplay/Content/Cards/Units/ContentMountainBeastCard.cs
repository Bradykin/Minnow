using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMountainBeastCard : GameUnitCard
{
    public ContentMountainBeastCard()
    {
        m_unit = new ContentMountainBeast();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Scaler);
        m_tags.AddTag(GameTag.TagType.Tank);
        m_tags.AddTag(GameTag.TagType.Healing);
    }
}
