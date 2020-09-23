using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentStoneGolemCard : GameCardEntityBase
{
    public ContentStoneGolemCard()
    {
        m_entity = new ContentStoneGolem();

        FillBasicData();

        m_playDesc = "Stone stands unbreaking.";
        m_cost = 2;
    }
}
