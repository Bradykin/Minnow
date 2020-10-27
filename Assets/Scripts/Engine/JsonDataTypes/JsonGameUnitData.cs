using System;

[Serializable]
public class JsonGameUnitData
{
    //GameElementBase values
    public string name;

    //GameUnit data
    public int team;
    public int curHealth;
    public int curStamina;
    public int maxHealth;
    public int staminaRegen;
    public int maxStamina;
    public int power;
    public int typeline;
    public JsonKeywordHolderData keywordHolderJson;
    public int staminaToAttack;
    public int sightRange;
}