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
        Special
    }

    protected string m_name { get; set; }
    protected string m_desc = string.Empty;
    public Sprite m_icon { get; protected set; }
    public GameRarity m_rarity { get; protected set; } = GameRarity.Common;
    public GameTagHolder m_tagHolder = new GameTagHolder();

    public virtual string GetName()
    {
        return m_name;
    }

    public string GetBaseName()
    {
        return m_name;
    }
}
