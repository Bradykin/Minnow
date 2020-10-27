using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameAction : GameElementBase, ISave<JsonActionData>, ILoad<JsonActionData>
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

    public abstract string GetDesc();

    public abstract void AddAction(GameAction toAdd);

    public abstract void DoAction();

    public abstract JsonActionData SaveToJson();

    public abstract void LoadFromJson(JsonActionData jsonData);
}
