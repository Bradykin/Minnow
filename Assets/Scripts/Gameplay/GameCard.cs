using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameCard : GameElementBase
{
    public virtual void PlayCard() { }
    public virtual void PlayCard(GameTile targetTile) { }
    public virtual void PlayCard(GameEntityBase targetEntity) { }

    public virtual bool IsValidToPlay() { return false; }
    public virtual bool IsValidToPlay(GameTile targetTile) { return false; }
    public virtual bool IsValidToPlay(GameEntityBase targetEntity) { return false; }
}
