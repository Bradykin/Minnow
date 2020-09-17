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
        m_desc = "" + m_numShields;
        m_focusInfoText = "Prevent all damage from this many attacks.";
        m_keywordParamType = KeywordParamType.IntParam;
    }

    public void IncreaseShield(int increase)
    {
        m_numShields += increase;

        m_name = "Damage Shield";
        m_desc = "" + m_numShields;
    }

    public void DecreaseShield(int decrease)
    {
        m_numShields -= decrease;

        m_name = "Damage Shield";
        m_desc = "" + m_numShields;
    }

    public override string SaveToJson()
    {
        JsonKeywordData jsonData = new JsonKeywordData
        {
            name = m_name,
            intValue = m_numShields
        };

        var export = JsonUtility.ToJson(jsonData);

        return export;
    }

    public override void LoadFromJson(JsonKeywordData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
