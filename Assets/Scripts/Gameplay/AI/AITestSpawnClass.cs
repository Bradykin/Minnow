using System.Collections;
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
                Globals.m_testSpawnEnemyEntity = new ContentShadeEnemy(null);
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                GameHelper.GetPlayer().AddRelic(new ContentDesignSchematicsRelic());
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                GameHelper.GetPlayer().AddCardToHand(GameCardFactory.GetCardClone(new ContentWildfolkCard()), true);
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                GameHelper.GetPlayer().AddCardToHand(GameCardFactory.GetCardClone(new ContentBloodSacrificeCard()), true);
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

            if (Input.GetKeyDown(KeyCode.U))
            {
                if (Globals.m_selectedEnemy != null)
                {
                    GameEnemyEntity gameEnemyEntity = Globals.m_selectedEnemy.GetEntity() as GameEnemyEntity;
                    Globals.m_focusedDebugEnemyEntity = gameEnemyEntity;
                    AIGameEnemyEntity AIGameEnemyEntity = gameEnemyEntity.m_AIGameEnemyEntity;
                    for (int i = 0; i < AIGameEnemyEntity.m_AIDebugLogs.Count; i++)
                    {
                        Debug.Log(AIGameEnemyEntity.m_AIDebugLogs[i]);
                    }
                }
            }
        }
    }
}
