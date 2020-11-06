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
                GameHelper.GetPlayer().AddRelic(new ContentBeadofJoyRelic());
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                Globals.m_testSpawnEnemyUnit = new ContentCrumblingAncientEnemy(null);
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                GameHelper.GetPlayer().AddCardToHand(GameCardFactory.GetCardClone(new ContentFirestormCard()), true);
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                GameHelper.GetPlayer().AddCardToHand(GameCardFactory.GetCardClone(new ContentDreamCard()), true);
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                GameHelper.GetPlayer().AddCardToHand(GameCardFactory.GetCardClone(new ContentNightWingsCard()), true);
            }
            
            if (Input.GetKeyDown(KeyCode.H))
            {
                UICameraController.Instance.SmoothCameraTransitionToGameObject(GameHelper.GetPlayer().GetCastleWorldTile().gameObject);
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                GameHelper.GetPlayer().AddRelic(new ContentEyeOfTelloRelic());
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                GameHelper.GetPlayer().AddCardToHand(GameCardFactory.GetCardClone(new ContentShivCard()), false);
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
