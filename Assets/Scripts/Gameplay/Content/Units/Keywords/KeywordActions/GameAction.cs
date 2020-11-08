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
        UnitListIntParam,
        UnitIntListIntParam,
        UnitTwoIntListIntParam,
        UnitKeywordParam,
        UnitListIntKeywordParam,
        GameWalletParam
    }

    public ActionParamType m_actionParamType;

    public abstract string GetDesc();

    public abstract void DoAction();

    public abstract void AddAction(GameAction toAdd);

    public abstract void SubtractAction(GameAction toSubtract);

    public abstract bool ShouldBeRemoved();

    public abstract GameUnit GetGameUnit();

    public abstract JsonActionData SaveToJson();

    public abstract void LoadFromJson(JsonActionData jsonData);
}
