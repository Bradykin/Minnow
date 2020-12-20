using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHeroCard : GameUnitCard
{
    public ContentHeroCard()
    {
        m_unit = new ContentHero();

        m_cost = 4;

        FillBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Scaler);
    }
}
