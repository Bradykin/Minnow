using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWarriorPriestessCard : GameUnitCard
{
    public ContentWarriorPriestessCard()
    {
        m_unit = new ContentWarriorPriestess();

        m_cost = 3;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Healing);
        m_tags.AddTag(GameTag.TagType.Tank);
    }
}
