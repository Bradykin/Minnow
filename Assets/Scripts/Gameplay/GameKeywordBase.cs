using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameKeywordBase : GameElementBase, ISave, ILoad<JsonKeywordData>
{
    public abstract string SaveToJson();

    public abstract void LoadFromJson(JsonKeywordData jsonData);
}
