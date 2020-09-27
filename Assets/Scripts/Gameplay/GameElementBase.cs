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
    public string m_desc { get; protected set; }
    public Sprite m_icon { get; protected set; }
    public GameRarity m_rarity { get; protected set; } = GameRarity.Common;
    public GameTag m_tags = new GameTag();

    public virtual void ClearTooltip()
    {
        UITooltipController tooltipController = UITooltipController.Instance;
        tooltipController.gameObject.SetActive(false);
    }
}
