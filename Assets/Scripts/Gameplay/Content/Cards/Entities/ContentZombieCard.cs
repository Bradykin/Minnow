using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentZombieCard : GameUnitCardBase
{
    public ContentZombieCard()
    {
        m_unit = new ContentZombie();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Midrange);
        m_tags.AddTag(GameTag.TagType.Tank);
    }
}
