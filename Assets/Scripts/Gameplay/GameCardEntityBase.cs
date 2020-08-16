using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCardEntityBase : GameCard
{
    protected GameEntityBase m_entity { get; set; }

    public GameEntityBase GetEntity()
    {
        return m_entity;
    }
}
