public struct JsonGameEntityData
{
    //GameElementBase values
    public string name;

    //GameEntity data
    public int team;
    public int curHealth;
    public int curAP;
    public int maxHealth;
    public int apRegen;
    public int maxAP;
    public int power;
    public int typeline;
    public JsonKeywordHolderData keywordHolder; // NOT IN JSON YET
    public int apToAttack;
    public int sightRange;
}