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
                Globals.m_testSpawnEnemyUnit = new ContentZombieEnemy(null);
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                GameHelper.GetPlayer().AddRelic(new ContentSoulTrapRelic());
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                GameHelper.GetPlayer().AddCardToHand(GameCardFactory.GetCardClone(new ContentGroundskeeperCard()), true);
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
                UICameraController.Instance.SmoothCameraTransitionToGameObject(GameHelper.GetPlayer().Castle.GetWorldTile().gameObject);
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
                    GameEnemyUnit gameEnemyUnit = Globals.m_selectedEnemy.GetUnit() as GameEnemyUnit;
                    Globals.m_focusedDebugEnemyUnit = gameEnemyUnit;
                    AIGameEnemyUnit AIGameEnemyUnit = gameEnemyUnit.m_AIGameEnemyUnit;
                    for (int i = 0; i < AIGameEnemyUnit.m_AIDebugLogs.Count; i++)
                    {
                        Debug.Log(AIGameEnemyUnit.m_AIDebugLogs[i]);
                    }
                }
            }
        }
    }
}
