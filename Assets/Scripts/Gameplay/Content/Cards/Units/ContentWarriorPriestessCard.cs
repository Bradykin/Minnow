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

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Healing, 2);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.Tank);
    }
}
