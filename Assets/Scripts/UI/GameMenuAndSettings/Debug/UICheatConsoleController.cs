using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

public class UICheatConsoleController : Singleton<UICheatConsoleController>
{
    public GameObject m_consoleHolder;

    void Start()
    {
        if (!Constants.CheatsOn)
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.BackQuote) || Input.GetKeyUp(KeyCode.F2))
        {
            m_consoleHolder.SetActive(!m_consoleHolder.activeSelf);
        }
    }

    public void TriggerCheat(string msg)
    {
        string cheat = "";
        int indexOfSpace = msg.IndexOf(" ");
        if (indexOfSpace > 0)
        {
            cheat = msg.Substring(0, msg.IndexOf(" "));
        }
        else
        {
            cheat = msg;
        }
        string param = "";
        int paramIndex = msg.IndexOf(" ") + 1;
        if (paramIndex > 0)
        {
            param = msg.Substring(paramIndex);
        }

        HandleCheats(cheat, param);
    }

    private void HandleCheats(string cheat, string param)
    {
        if (cheat == "AddRelic")
        {
            HandleAddRelic(param);
            return;
        }

        if (cheat == "AddCard")
        {
            HandleAddCard(param);
            return;
        }

        if (cheat == "AddEnemy")
        {
            HandleAddEnemy(param);
            return;
        }

        if (cheat == "EndWave")
        {
            HandleEndWave();
            return;
        }

        Debug.Log(cheat + " is an invalid cheat command.");
    }


    //Implementations
    private void HandleAddRelic(string param)
    {
        GameRelic newRelic = GameRelicFactory.GetRelicByName(param);
        if (newRelic == null)
        {
            Debug.Log(param + " is an invalid relic name.");
            return;
        }

        GameHelper.GetPlayer().AddRelic(newRelic);
    }

    private void HandleAddCard(string param)
    {
        GameCard newCard = GameCardFactory.GetCardByName(param);
        if (newCard == null)
        {
            Debug.Log(param + " is an invalid card name.");
            return;
        }

        GameHelper.GetPlayer().AddCardToHand(newCard, true);
    }

    private void HandleAddEnemy(string param)
    {
        GameEnemyUnit newEnemy = GameUnitFactory.GetEnemyFromName(param);
        if (newEnemy == null)
        {
            Debug.Log(param + " is an invalid enemy name.");
            return;
        }

        Globals.m_testSpawnEnemyUnit = newEnemy;
    }

    private void HandleEndWave()
    {
        WorldController.Instance.m_gameController.StartIntermissionCheat();
    }
}
