using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOpponent : ITurns
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //============================================================================================================//

    public void StartTurn()
    {
        Debug.Log("Start opponent turn");
    }

    public void EndTurn()
    {
        Debug.Log("End opponent turn");
    }
}
