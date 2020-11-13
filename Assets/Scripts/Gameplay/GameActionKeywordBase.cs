using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameActionKeywordBase : GameKeywordBase
{
    protected List<GameAction> m_actions = new List<GameAction>();

    public virtual bool IsEmpty()
    {
        if (m_actions.Count == 0)
        {
            return true;
        }

        bool anyNotNull = false;
        for (int i = 0; i < m_actions.Count; i++)
        {
            if (m_actions[i] != null)
            {
                anyNotNull = true;
            }
        }

        return !anyNotNull;
    }

    public override void AddKeyword(GameKeywordBase toAdd)
    {
        GameActionKeywordBase tempKeyword = (GameActionKeywordBase)toAdd;

        for (int i = 0; i < tempKeyword.m_actions.Count; i++)
        {
            bool isAddingAction = false;
            for (int c = 0; c < m_actions.Count; c++)
            {
                if (m_actions[c] == null)
                {
                    continue;
                }

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

    public override void SubtractKeyword(GameKeywordBase toSubtract)
    {
        GameActionKeywordBase tempKeyword = (GameActionKeywordBase)toSubtract;

        for (int i = 0; i < tempKeyword.m_actions.Count; i++)
        {
            for (int c = 0; c < m_actions.Count; c++)
            {
                if (m_actions[c].GetName() == tempKeyword.m_actions[i].GetName())
                {
                    switch (tempKeyword.m_actions[i].m_actionParamType)
                    {
                        case GameAction.ActionParamType.NoParams:
                        case GameAction.ActionParamType.UnitParam:
                            m_actions.RemoveAt(c);
                            break;
                        case GameAction.ActionParamType.IntParam:
                        case GameAction.ActionParamType.TwoIntParam:
                        case GameAction.ActionParamType.UnitIntParam:
                        case GameAction.ActionParamType.UnitTwoIntParam:
                        case GameAction.ActionParamType.UnitListIntParam:
                        case GameAction.ActionParamType.UnitIntListIntParam:
                        case GameAction.ActionParamType.UnitKeywordParam:
                        case GameAction.ActionParamType.UnitListIntKeywordParam:
                        case GameAction.ActionParamType.GameWalletParam:
                            m_actions[c].SubtractAction(tempKeyword.m_actions[i]);

                            if (m_actions[c].ShouldBeRemoved())
                            {
                                m_actions.RemoveAt(c);
                            }
                            break;
                    }
                    break;
                }
            }
        }
    }

    public override bool ShouldBeRemoved()
    {
        return IsEmpty();
    }

    public virtual void DoAction()
    {
        for (int i = 0; i < m_actions.Count; i++)
        {
            if (m_actions[i] == null)
            {
                continue;
            }

            m_actions[i].DoAction();
        }
    }

    public override string GetDesc()
    {
        string toReturn = "";

        for (int i = 0; i < m_actions.Count; i++)
        {
            if (m_actions[i] == null)
            {
                continue;
            }

            toReturn += m_actions[i].GetDesc();

            if (i != m_actions.Count -1)
            {
                toReturn += ", ";
            }
        }

        return toReturn;
    }

    public override JsonGameKeywordData SaveToJson()
    {
        JsonGameKeywordData jsonData = new JsonGameKeywordData
        {
            name = m_name,
            gameActionData = new List<JsonGameActionData>()
        };

        for (int i = 0; i < m_actions.Count; i++)
        {
            jsonData.gameActionData.Add(m_actions[i].SaveToJson());
        }

        return jsonData;
    }

    public override void LoadFromJson(JsonGameKeywordData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
