using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

public class WorldController : Singleton<WorldController>
{
    public GameController m_gameController { get; private set; }

    void Start()
    {
        m_gameController = new GameController();
    }

    void Update()
    {
    }
}
