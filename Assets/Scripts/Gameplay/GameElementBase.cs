using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameElementBase
{
    public enum GameRarity : int
    {
        Common,
        Uncommon,
        Rare,
        Starter,
        Event
    }

    public string m_name { get; protected set; }
    protected string m_desc = string.Empty;
    public Sprite m_icon { get; protected set; }
    public GameRarity m_rarity { get; protected set; } = GameRarity.Common;
    public GameTag m_tags = new GameTag();
}
