using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDamageShieldKeyword : GameKeywordBase
{
    public int m_numShields;

    public GameDamageShieldKeyword(int numShields)
    {
        m_numShields = numShields;

        m_name = "Damage Shield";
        m_focusInfoText = "Prevent all damage from this many attacks. <i>(Stacks)</i>";
        m_keywordParamType = KeywordParamType.IntParam;
    }

    public override void AddKeyword(GameKeywordBase toAdd)
    {
        GameDamageShieldKeyword tempKeyword = (GameDamageShieldKeyword)toAdd;

        m_numShields += tempKeyword.m_numShields;
    }

    public override string GetDesc()
    {
        return "" + m_numShields;
    }

    public void DecreaseShield(int decrease)
    {
        m_numShields -= decrease;
    }

    public override string SaveToJsonAsString()
    {
        JsonKeywordData jsonData = new JsonKeywordData
        {
            name = m_name,
            intValue = m_numShields
        };

        var export = JsonConvert.SerializeObject(jsonData);

        return export;
    }

    public override void LoadFromJson(JsonKeywordData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
