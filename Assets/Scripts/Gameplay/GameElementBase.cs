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

    protected string m_name { get; set; }
    protected string m_desc = string.Empty;
    public Sprite m_icon { get; protected set; }
    public GameRarity m_rarity { get; protected set; } = GameRarity.Common;
    public GameTag m_tags = new GameTag();

    public virtual string GetName()
    {
        return m_name;
    }
}
