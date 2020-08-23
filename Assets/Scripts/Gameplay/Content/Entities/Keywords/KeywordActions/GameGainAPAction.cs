using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGainAPAction : GameAction
{
    private GameEntity m_entity;
    private int m_toGain;

    public GameGainAPAction(GameEntity entity, int toGain)
    {
        m_entity = entity;
        m_toGain = toGain;

        m_name = "Gain AP";
        m_desc = "+ " + m_toGain + " AP";
    }

    public override void DoAction()
    {
        m_entity.GainAP(m_toGain);
    }
}
