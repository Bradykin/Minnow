using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Util
{
    public interface ITakeTurnInCoroutineAI
    {
        void SetupTurn();

        void CleanupTurn();
        
        IEnumerator TakeTurn(bool yield);
    }
}