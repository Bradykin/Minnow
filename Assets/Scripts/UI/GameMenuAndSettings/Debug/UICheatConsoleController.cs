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
        if (Input.GetKeyUp(KeyCode.BackQuote))
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

        if (cheat == "ClearPlayerAccountData")
        {
            HandleClearPlayerAccountData();
            return;
        }

        if (cheat == "ToggleFog")
        {
            ToggleFog();
            return;
        }

        if (cheat == "SetRandomStarterCardLevels")
        {
            SetRandomStarterCardLevels(param);
            return;
        }

        if (cheat == "SetCurrentWave")
        {
            SetCurrentWave(param);
            return;
        }

        if (cheat == "SetCurrentTurn")
        {
            SetCurrentTurn(param);
            return;
        }

        Debug.Log($"{cheat} is an invalid cheat command.");
    }


    //Implementations
    private void HandleAddRelic(string param)
    {
        GameRelic newRelic = GameRelicFactory.GetRelicByName(param);
        if (newRelic == null)
        {
            Debug.Log($"{param} is an invalid relic name.");
            return;
        }

        GameHelper.GetPlayer().AddRelic(newRelic);
    }

    private void HandleAddCard(string param)
    {
        GameCard newCard = GameCardFactory.GetCardByName(param);
        if (newCard == null)
        {
            Debug.Log($"{param} is an invalid card name.");
            return;
        }

        GameHelper.GetPlayer().AddCardToHand(newCard, true);
    }

    private void HandleClearPlayerAccountData()
    {
        PlayerDataManager.ClearPlayerAccountData();

        Files.ClearPlayerAccountData();

        if (Constants.DebugRandomStarterLevels)
        {
            PlayerDataManager.RandomizeStarterCardLevels();
        }

        UIStarterCardSelectionController.Instance.ResetStarterCardInit();
    }

    private void ToggleFog()
    {
        Constants.DebugSeeAllThroughFog = !Constants.DebugSeeAllThroughFog;
    }

    private void SetRandomStarterCardLevels(string param)
    {
        if (param == "True")
        {
            Constants.DebugRandomStarterLevels = true;
            return;
        }

        if (param == "False")
        {
            Constants.DebugRandomStarterLevels = false;
            return;
        }

        Debug.Log($"{param} is an invalid input, must be True or False");
    }

    private void SetCurrentWave(string param)
    {
        if (int.TryParse(param, out int result))
        {
            GameHelper.GetGameController().m_currentWaveNumber = result;
            return;
        }

        Debug.Log($"{param} is an invalid input. Must be a number.");
    }

    private void SetCurrentTurn(string param)
    {
        if (int.TryParse(param, out int result))
        {
            GameHelper.GetGameController().m_currentTurnNumber = result;
            return;
        }

        Debug.Log($"{param} is an invalid input. Must be a number.");
    }
}
