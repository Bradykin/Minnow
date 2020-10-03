using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameAction : GameElementBase
{
    public enum ActionParamType : int
    {
        NoParams,
        IntParam,
        TwoIntParam,
        UnitParam,
        UnitIntParam,
        UnitTwoIntParam,
        GameWalletParam
    }

    public ActionParamType m_actionParamType;

    public abstract void DoAction();

    public abstract string SaveToJson();

    public abstract void LoadFromJson(JsonActionData jsonData);
}
