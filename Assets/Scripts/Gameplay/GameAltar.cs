using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameAltar : GameEvent
{
    protected GameRelic m_altarRelic;

    public GameRelic GetAltarRelic()
    {
        return m_altarRelic;
    }

    public override string GetOptionOneTooltip()
    {
        //Left as stub.  Altars don't have options.
        return "";
    }

    public override string GetOptionTwoTooltip()
    {
        //Left as stub.  Altars don't have options.
        return "";
    }
}
