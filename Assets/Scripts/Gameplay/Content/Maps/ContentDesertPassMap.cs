﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertPassMap : GameMap
{
    public ContentDesertPassMap()
    {
        m_name = "Desert Pass";
        m_desc = "Can you protect the passes?";

        m_difficulty = MapDifficulty.Medium;

        m_id = 2;

        Init();
    }

    protected override void FillMapEvents()
    {
        //Need to fill
    }

    protected override void FillExclusionCardPool()
    {

    }

    protected override void FillEventPool()
    {
        FillBasicEventPool();
    }

    protected override void FillExclusionRelicPool()
    {

    }
}