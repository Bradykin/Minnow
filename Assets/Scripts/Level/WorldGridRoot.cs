using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGridRoot : MonoBehaviour, IReset
{
    public virtual void Activate()
    {
        WorldGridManager.Instance.Setup(transform);
    }

    public virtual void Reset()
    {
        WorldGridManager.Instance.RecycleGrid();        
    }
}
