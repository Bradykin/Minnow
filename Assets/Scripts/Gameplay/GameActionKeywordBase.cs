using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameActionKeywordBase : GameKeywordBase
{
    protected List<GameAction> m_actions = new List<GameAction>();

    public override void AddKeyword(GameKeywordBase toAdd)
    {
        GameActionKeywordBase tempKeyword = (GameActionKeywordBase)toAdd;

        for (int i = 0; i < tempKeyword.m_actions.Count; i++)
        {
            bool isAddingAction = false;
            for (int c = 0; c < m_actions.Count; c++)
            {
                if (m_actions[c].GetName() == tempKeyword.m_actions[i].GetName())
                {
                    m_actions[c].AddAction(tempKeyword.m_actions[i]);
                    isAddingAction = true;
                    break;
                }
            }

            if (!isAddingAction)
            {
                m_actions.Add(tempKeyword.m_actions[i]);
            }
        }
    }

    public virtual void DoAction()
    {
        for (int i = 0; i < m_actions.Count; i++)
        {
            m_actions[i].DoAction();
        }
    }

    public override string GetDesc()
    {
        string toReturn = "";

        for (int i = 0; i < m_actions.Count; i++)
        {
            toReturn += m_actions[i].GetDesc();

            if (i != m_actions.Count -1)
            {
                toReturn += ", ";
            }
        }

        return toReturn;
    }

    public override JsonKeywordData SaveToJson()
    {
        JsonKeywordData jsonData = new JsonKeywordData
        {
            name = m_name,
            actionJson = new List<JsonActionData>()
        };

        for (int i = 0; i < m_actions.Count; i++)
        {
            jsonData.actionJson.Add(m_actions[i].SaveToJson());
        }

        return jsonData;
    }

    public override void LoadFromJson(JsonKeywordData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
