using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayer : MonoBehaviour
{
    private GamePlayer m_player;

    void Start()
    {
        m_player = new GamePlayer();

        for (int i = 0; i < m_player.m_hand.Count; i++)
        {
            //Make the UICards here.
        }
    }
}
