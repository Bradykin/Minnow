using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGridRoot : MonoBehaviour, IReset
{
    public void Activate()
    {
        WorldGridManager.Instance.Setup(transform);
    }

    public void Reset()
    {
        WorldGridManager.Instance.RecycleGrid();        
    }
}
