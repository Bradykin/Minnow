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

    //For events
    public JsonGameEventData gameEventData;

    //For altars
    public JsonGameEventData gameAltarData;

    //For gold
    public int goldValue;
}