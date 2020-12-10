using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFrogShamanCard : GameUnitCard
{
    public ContentFrogShamanCard()
    {
        m_unit = new ContentFrogShaman();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Midrange);
        m_tags.AddTag(GameTag.TagType.Forest);
    }
}
