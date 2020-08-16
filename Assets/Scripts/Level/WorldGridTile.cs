using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGridTile : WorldElementBase
{
    public Vector2Int GridPosition;
    private GameTile m_gameTile;
    private SpriteRenderer m_renderer;

    public bool IsPassable => true;
    public int CostToPass => 1;

    void Start()
    {
        m_gameTile = new GameTile();
        m_renderer = GetComponent<SpriteRenderer>();

        //nmartino - Testing data for tooltips, feel free to remove if needed.
        m_gameElement = new GameGrassTerrain();

        m_renderer.color = m_gameElement.GetColor();
    }

    public void Init(int x, int y)
    {
        GridPosition = new Vector2Int(x, y);
    }
}
