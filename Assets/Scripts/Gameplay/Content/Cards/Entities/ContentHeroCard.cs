using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHeroCard : GameCardEntityBase
{
    public ContentHeroCard()
    {
        m_entity = new ContentHero();

        m_cost = 4;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Scaler);
    }
}
