﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventLeaveOption : GameEventOption
{
    public GameEventLeaveOption()
    {
        m_message = "Leave";
    }

    public GameEventLeaveOption(string message)
    {
        m_message = message;
    }

    public override void AcceptOption()
    {
        UIEventController.Instance.EndEvent();
    }
}
