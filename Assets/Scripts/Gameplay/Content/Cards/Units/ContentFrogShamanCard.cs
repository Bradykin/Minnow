using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFrogShamanCard : GameUnitCard
{
    public ContentFrogShamanCard()
    {
        m_unit = new ContentFrogShaman();

        m_cost = 3;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Water);
        m_tags.AddTag(GameTag.TagType.UtilitySpell);
    }
}
