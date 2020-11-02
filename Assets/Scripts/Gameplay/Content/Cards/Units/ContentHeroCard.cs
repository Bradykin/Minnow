using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHeroCard : GameUnitCard
{
    public ContentHeroCard()
    {
        m_unit = new ContentHero();

        m_cost = 4;
        m_playerUnlockLevel = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Scaler);
    }
}
