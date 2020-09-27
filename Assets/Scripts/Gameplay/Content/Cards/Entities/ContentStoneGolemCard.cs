using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentStoneGolemCard : GameCardEntityBase
{
    public ContentStoneGolemCard()
    {
        m_entity = new ContentStoneGolem();

        m_cost = 2;

        FillBasicData();
    }
}
