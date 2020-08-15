using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : WorldElementBase
{
    public Vector2Int GridPosition;

    void Start()
    {
        //nmartino - Testing data for tooltips, feel free to remove if needed.
        m_gameElement = new GameGoblinEntity();
    }

    public void Init(int x, int y)
    {
        GridPosition = new Vector2Int(x, y);
    }
}
