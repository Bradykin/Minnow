using System;

[Serializable]
public class JsonGameUnitData
{
    //GameElementBase values
    public string baseName;

    //GameUnit data
    public string customName;
    public int team;
    public int curHealth;
    public int curStamina;
    public int maxHealth;
    public int staminaRegen;
    public int maxStamina;
    public int power;
    public int typeline;
    public JsonGameKeywordHolderData jsonGameKeywordHolderData;
    public int staminaToAttack;
    public int sightRange;
    public string guid;
}