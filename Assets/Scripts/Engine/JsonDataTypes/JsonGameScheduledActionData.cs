using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct JsonGameScheduledActionData
{
    public int scheduledActionTime;
    public JsonGameActionData jsonActionData;

    public JsonGameUnitData jsonTargetUnitData;
}
