using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHeroCard : GameCardEntityBase
{
    public ContentHeroCard()
    {
        m_entity = new ContentHero();

        FillBasicData();

        m_playDesc = "Fear not, a hero has arrived.";
        m_cost = 4;
    }
}
