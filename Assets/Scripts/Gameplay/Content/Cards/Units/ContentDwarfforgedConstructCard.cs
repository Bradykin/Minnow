﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDwarfforgedConstructCard : GameUnitCard
{
    public ContentDwarfforgedConstructCard()
    {
        m_unit = new ContentDwarfforgedConstruct();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Midrange);
        m_tags.AddTag(GameTag.TagType.Forest);
    }
}
