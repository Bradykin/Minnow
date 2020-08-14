using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    public Vector2Int GridPosition;

    public void Init(int x, int y)
    {
        GridPosition = new Vector2Int(x, y);
    }
}
