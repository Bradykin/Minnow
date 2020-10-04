﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertRedGrassDunesTerrain : GameTerrainBase
{
    public ContentDesertRedGrassDunesTerrain()
    {
        m_damageReduction = Constants.HalfDunesDamageReduction;
        m_costToPass = Constants.HalfDunesMovementCost;

        m_name = "DesertRedGrassDunes";
        m_desc = GenerateDescription();
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isHot = true;

        LateInit();
    }
}