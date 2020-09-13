using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITestSpawnClass : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Globals.m_testSpawnEnemyEntity = new ContentSpinnerEnemy(null);
        }
    }
}
