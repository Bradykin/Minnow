using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITestSpawnClass : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Globals.m_testSpawnEnemyEntity = new ContentToadEnemy(null);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            GameHelper.GetPlayer().AddRelic(new ContentBurningShivsRelic());
            GameHelper.GetPlayer().AddRelic(new ContentPoisonedShivsRelic());
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            GameHelper.GetPlayer().AddCardToHand(GameCardFactory.GetCardClone(new ContentPurpleBeamSwordCard()), true);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            GameHelper.GetPlayer().AddCardToHand(GameCardFactory.GetCardClone(new ContentAegisCard()), true);
        }
    }
}
