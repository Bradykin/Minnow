using System;

[Serializable]
public class JsonGameWorldPerkData
{
    //GameElementBase values
    public string baseName;

    //GameUnit data
    public int perkType;

    //For chests
    public int chestRarity;
    public JsonGameEventData gameEventData;
}