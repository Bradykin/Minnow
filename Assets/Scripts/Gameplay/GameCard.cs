using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameCard : GameElementBase
{
    public int m_cost;
    public string m_typeline;
    public string m_playDesc;

    public virtual void PlayCard() { }
    public virtual void PlayCard(GameTile targetTile) { }
    public virtual void PlayCard(GameEntity targetEntity) { }

    public virtual bool IsValidToPlay() { return false; }
    public virtual bool IsValidToPlay(GameTile targetTile) { return false; }
    public virtual bool IsValidToPlay(GameEntity targetEntity) { return false; }
}
