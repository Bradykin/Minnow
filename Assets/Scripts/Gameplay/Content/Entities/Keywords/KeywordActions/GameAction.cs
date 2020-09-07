using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameAction : GameElementBase
{
    public enum ActionParamType : int
    {
        NoParams,
        IntParam,
        EntityParam,
        EntityIntParam,
        EntityTwoIntParam
    }

    public ActionParamType m_keywordParamType;

    public abstract void DoAction();
}
