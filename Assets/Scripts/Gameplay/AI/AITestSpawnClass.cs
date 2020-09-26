﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITestSpawnClass : MonoBehaviour
{
    void Update()
    {
        if (Constants.CheatsOn)
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                Globals.m_testSpawnEnemyEntity = new ContentSlimeEnemy(null);
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                GameHelper.GetPlayer().AddRelic(new ContentBestialWrathRelic());
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                GameHelper.GetPlayer().AddCardToHand(GameCardFactory.GetCardClone(new ContentDevourerCard()), true);
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                GameHelper.GetPlayer().AddCardToHand(GameCardFactory.GetCardClone(new ContentFuryCard()), true);
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                GameHelper.GetPlayer().AddCardToHand(GameCardFactory.GetCardClone(new ContentRoarOfVictoryCard()), true);
            }
            
            if (Input.GetKeyDown(KeyCode.H))
            {
                GameHelper.GetPlayer().AddCardToHand(GameCardFactory.GetCardClone(new ContentCurseOfInactionCard()), true);
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                GameHelper.GetPlayer().m_wallet.m_gold += 50;
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                GameHelper.GetPlayer().AddEnergy(1);
            }
        }
    }
}
