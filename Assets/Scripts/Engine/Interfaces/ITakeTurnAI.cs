using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Util
{
    public interface ITakeTurnAI
    {
        IEnumerator TakeTurn(bool yield);
    }
}