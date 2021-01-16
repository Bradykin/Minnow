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
    public int permMaxHealth;
    public int staminaRegen;
    public int permStaminaRegen;
    public int maxStamina;
    public int permMaxStamina;
    public int attack;
    public int permAttack;
    public int typeline;
    public JsonGameKeywordHolderData jsonGameKeywordHolderData;
    public int staminaToAttack;
    public int sightRange;
    public string guid;

    //Unique case variables
    public int intValue;
    public bool boolValue1;
    public bool boolValue2;
}