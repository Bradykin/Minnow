using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentZombieCard : GameCardEntityBase
{
    public ContentZombieCard()
    {
        m_entity = new ContentZombie();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Midrange);
        m_tags.AddTag(GameTag.TagType.Tank);
    }
}
