using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct JsonGameBuildingData
{
    //Things currently being used

    //GameElementBase values
    public string name;

    //GameTerrain values
    public int curHealth;



    //Things not currently being used

    //GameElementBase values
    public string desc;
    public int rarity;
    public Color color;

    //GameBuilding values
    public int maxHealth;
    public int sightRange;
    public bool isDestroyed;
    public bool expandsPlaceRange;
}
