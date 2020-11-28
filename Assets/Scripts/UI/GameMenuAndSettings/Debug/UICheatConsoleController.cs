using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UICheatConsoleController : Singleton<UICheatConsoleController>
{
    public GameObject m_consoleHolder;
    public InputField m_inputField;

    void Start()
    {
        if (!Constants.DevMode)
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.BackQuote) || Input.GetKeyUp(KeyCode.F2))
        {
            m_consoleHolder.SetActive(!m_consoleHolder.activeSelf);

            if (m_consoleHolder.activeSelf)
            {
                m_inputField.Select();
            }
        }
    }

    public void TriggerCheat(string msg)
    {
        msg = msg.ToLower();
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
        if (cheat == "addrelic")
        {
            HandleAddRelic(param);
            return;
        }

        if (cheat == "addcard")
        {
            HandleAddCard(param);
            return;
        }

        if (cheat == "clearplayeraccountdata")
        {
            HandleClearPlayerAccountData();
            return;
        }

        if (cheat == "togglefog")
        {
            ToggleFog();
            return;
        }

        if (cheat == "setcurrentwave")
        {
            SetCurrentWave(param);
            return;
        }

        if (cheat == "setcurrentturn")
        {
            SetCurrentTurn(param);
            return;
        }

        if (cheat == "addenemy")
        {
            HandleAddEnemy(param);
            return;
        }

        if (cheat == "endwave")
        {
            HandleEndWave();
            return;
        }

        if (cheat == "addgold")
        {
            HandleAddGold(param);
            return;
        }

        if (cheat == "addenergy")
        {
            HandleAddEnergy(param);
            return;
        }

        if (cheat == "addactions")
        {
            HandleAddActions(param);
            return;
        }

        if (cheat == "removeactions")
        {
            HandleRemoveActions(param);
            return;
        }

        if (cheat == "setcastlehealth")
        {
            HandleSetCastleHealth(param);
            return;
        }

        if (cheat == "uber")
        {
            HandleUber();
            return;
        }

        if (cheat == "drawcard")
        {
            HandleDrawCard();
            return;
        }

        if (cheat == "loadgrid" || cheat == "loadmap")
        {
            LoadGrid(param);
            return;
        }

        if (cheat == "savegrid" || cheat == "savemap")
        {
            SaveGrid(param);
            return;
        }

        if (cheat == "wingame")
        {
            WinGame();
            return;
        }

        if (cheat == "losegame")
        {
            LoseGame();
            return;
        }

        if (cheat == "temp")
        {
            TempTest();
            return;
        }

        if (cheat == "addrep")
        {
            HandleAddRep(param);
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
            Debug.Log($"{param} is an invalid relic name.");
            return;
        }

        GameHelper.GetPlayer().AddRelic(newRelic);
    }

    private void HandleAddCard(string param)
    {
        if (param == "uberboar")
        {
            GameUnit uberBoar = new ContentNaturalScout();
            uberBoar.AddStats(9998, 9998, true, false);
            GameCard uberBoarCard = GameCardFactory.GetCardFromUnit(uberBoar);
            GameHelper.GetPlayer().AddCardToHand(uberBoarCard, true);
            return;
        }
        
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

        UIStarterCardSelectionController.Instance.ResetStarterCardInit();
    }

    private void ToggleFog()
    {
        Constants.DebugSeeAllThroughFog = !Constants.DebugSeeAllThroughFog;
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

    private void HandleAddGold(string param)
    {
        GameHelper.GetPlayer().m_wallet.AddResources(new GameWallet(int.Parse(param)));
    }

    private void HandleAddEnergy(string param)
    {
        GameHelper.GetPlayer().AddEnergy(int.Parse(param));
    }

    private void HandleAddActions(string param)
    {
        GameHelper.GetPlayer().AddBonusActions(int.Parse(param));
    }

    private void HandleRemoveActions(string param)
    {
        GameHelper.GetPlayer().SpendActions(int.Parse(param));
    }

    private void HandleDrawCard()
    {
        GameHelper.GetPlayer().DrawCard();
    }

    private void HandleSetCastleHealth(string param)
    {
        GameHelper.GetPlayer().GetCastleGameTile().GetBuilding().m_maxHealth = int.Parse(param);
        GameHelper.GetPlayer().GetCastleGameTile().GetBuilding().m_curHealth += int.Parse(param);
    }

    private void HandleUber()
    {
        HandleAddGold("10000");
        HandleAddEnergy("10000");
        HandleAddActions("10");

        HandleSetCastleHealth("10000");
    }

    private void HandleEndWave()
    {
        WorldController.Instance.m_gameController.StartIntermissionCheat();
    }

    private void HandleAddRep(string param)
    {
        PlayerDataManager.HandleEXPGain(int.Parse(param));
    }

    private void LoadGrid(string param)
    {
        if (GameHelper.IsInLevelBuilder())
        {
            LevelCreator levelCreator = FindObjectOfType<LevelCreator>();
            if (levelCreator != null)
            {
                levelCreator.LoadGrid(int.Parse(param));
            }
            else
            {
                Debug.LogWarning("Can't find LevelCreator");
            }
        }
        else
        {
            Debug.LogWarning("Wrong scene");
        }
    }

    private void SaveGrid(string param)
    {
        if (GameHelper.IsInLevelBuilder())
        {
            LevelCreator levelCreator = FindObjectOfType<LevelCreator>();
            if (levelCreator != null)
            {
                if (param == "" && levelCreator.m_curPathIndex >= 0)
                {
                    levelCreator.SaveGrid(levelCreator.m_curPathIndex);
                }

                levelCreator.SaveGrid(int.Parse(param));
            }
            else
            {
                Debug.LogWarning("Can't find LevelCreator");
            }
        }
        else
        {
            Debug.LogWarning("Wrong scene");
        }
    }

    private void WinGame()
    {
        GameHelper.EndLevel(RunEndType.Win);
    }

    private void LoseGame()
    {
        GameHelper.EndLevel(RunEndType.Loss);
    }

    private void TempTest()
    {
        UIRelicController.Instance.ClearRelics();
    }
}
