using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDamageShieldKeyword : GameKeywordBase
{
    private int m_numShields;

    public GameDamageShieldKeyword(int numShields)
    {
        m_numShields = numShields;

        m_name = "Damage Shield";
        m_focusInfoText = "Prevent all damage from this many attacks. <i>(Stacks)</i>";
        m_keywordParamType = KeywordParamType.IntParam;
    }

    public override string GetDesc()
    {
        return "" + m_numShields;
    }

    public int GetShieldLevel()
    {
        return m_numShields;
    }

    public override void AddKeyword(GameKeywordBase toAdd)
    {
        GameDamageShieldKeyword tempKeyword = (GameDamageShieldKeyword)toAdd;

        m_numShields += tempKeyword.m_numShields;
    }

    public override void SubtractKeyword(GameKeywordBase toSubtract)
    {
        GameDamageShieldKeyword tempKeyword = (GameDamageShieldKeyword)toSubtract;

        m_numShields -= tempKeyword.m_numShields;
    }

    public void DecreaseShield(int decrease)
    {
        m_numShields -= decrease;
    }

    public override bool ShouldBeRemoved()
    {
        return m_numShields <= 0;
    }

    public override JsonGameKeywordData SaveToJson()
    {
        JsonGameKeywordData jsonData = new JsonGameKeywordData
        {
            name = m_name,
            isPermanentValue = m_isPermanent,
            intValue = m_numShields
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonGameKeywordData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
