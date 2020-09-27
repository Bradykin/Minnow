using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;
using UnityEngine.SceneManagement;

public class WorldController : Singleton<WorldController>
{
    public GameController m_gameController { get; private set; }

    public List<UICard> m_playerHand { get; private set; }

    private bool m_hasSpawnedEliteThisWave;
    private bool m_hasSpawnedBoss;
    private int m_playerEntityFocusIndex;

    void Start()
    {
        m_gameController = new GameController();
        m_playerHand = new List<UICard>();

        m_playerEntityFocusIndex = 0;
    }

    void Update()
    {
        HandlePlayerHand();

        if (Input.GetKeyUp(KeyCode.Q))
        {
            FocusPrevPlayerEntity();
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            FocusNextPlayerEntity();
        }
    }

    public void UpdateHand()
    {
        for (int i = 0; i < m_playerHand.Count; i++)
        {
            m_playerHand[i].SetCardData();
        }
    }

    public void PlayCard(UICard card)
    {
        Recycler.Recycle<UICard>(card);
        Globals.m_selectedCard = null;

        m_gameController.m_player.PlayCard(card.m_card);

        m_playerHand.Remove(card);
    }

    public void PostPlayCard()
    {
        WorldController.Instance.UpdateHand();
    }

    private void HandlePlayerHand()
    {
        List<GameCard> playerHand = m_gameController.m_player.m_hand;

        for (int i = 0; i < playerHand.Count; i++)
        {
            if (!HasUICardForGameCard(playerHand[i]))
            {
                UICard obj = FactoryManager.Instance.GetFactory<UICardFactory>().CreateObject<UICard>(playerHand[i], UICard.CardDisplayType.Hand);

                m_playerHand.Insert(i, obj);
            }
        }

        for (int i = m_playerHand.Count - 1; i >= 0; i--)
        {
            if (m_playerHand[i] == null)
            {
                m_playerHand.RemoveAt(i);
            }
        }

        int bigCardIndex = -1;
        for (int i = 0; i < m_playerHand.Count; i++)
        {
            if (m_playerHand[i].GetIsBig())
            {
                bigCardIndex = i;
            }
        }

        float xPosBase = -500.0f;
        float xPosMult = (120.0f - (Mathf.Max((m_playerHand.Count - Constants.InitialHandSize), 0) * 5f));
        float yPos = -350.0f;
        float yPosBigOffset = 125.0f;
        float zPos = 0.0f;
        float smallCardScale = 0.75f;
        float bigCardScale = 1.25f;
        float splitDif = 0.1f;

        if (bigCardIndex == -1)
        {
            for (int i = 0; i < m_playerHand.Count; i++)
            {
                Vector3 pos = new Vector3(xPosBase + i * xPosMult, yPos, zPos);
                m_playerHand[i].gameObject.transform.localPosition = pos;
                m_playerHand[i].gameObject.transform.localScale = new Vector3(smallCardScale, smallCardScale, smallCardScale);
            }
        }
        else
        {
            for (int i = 0; i < m_playerHand.Count; i++)
            {
                Vector3 pos = new Vector3(0f,0f,0f);
                m_playerHand[i].gameObject.transform.localScale = new Vector3(smallCardScale, smallCardScale, smallCardScale);

                if (i < bigCardIndex)
                {
                    pos = new Vector3((xPosBase * (1.0f + (splitDif/3.0f))) + i * xPosMult, yPos, zPos);
                }
                else if (i > bigCardIndex)
                {
                    pos = new Vector3((xPosBase * (0.9f - splitDif)) + i * xPosMult, yPos, zPos);
                }
                else
                {
                    pos = new Vector3(xPosBase + i * xPosMult, yPos + yPosBigOffset, zPos);
                    m_playerHand[i].gameObject.transform.localScale = new Vector3(bigCardScale, bigCardScale, bigCardScale);
                }

                m_playerHand[i].gameObject.transform.localPosition = pos;
            }
        }
    }

    private bool HasUICardForGameCard(GameCard card)
    {
        for (int i = 0; i < m_playerHand.Count; i++)
        {
            if (m_playerHand[i].m_card == card)
            {
                return true;
            }
        }
        
        return false;
    }

    public void MoveToNextTurn()
    {
        m_gameController.MoveToNextTurn();
    }

    public void ClearHand()
    {
        if (m_playerHand == null)
        {
            return;
        }
        
        for (int i = m_playerHand.Count - 1; i >= 0; i--)
        {
            Recycler.Recycle<UICard>(m_playerHand[i]);
            Globals.m_selectedCard = null;
            m_playerHand.RemoveAt(i);
        }
    }

    private void ClearAllEntities()
    {
        for (int i = 0; i < WorldGridManager.Instance.m_gridArray.Length; i++)
        {
            if (WorldGridManager.Instance.m_gridArray[i].GetGameTile().IsOccupied())
            {
                WorldGridManager.Instance.m_gridArray[i].RecycleEntity();
            }
        }

        m_gameController.m_gameOpponent.m_controlledEntities.Clear();
        m_gameController.m_player.m_controlledEntities.Clear();
    }

    public void StartIntermission()
    {
        GamePlayer player = m_gameController.m_player;

        Globals.m_spellsPlayedPreviousTurn = 0;

        UITooltipController.Instance.ClearTooltipStack();
        ClearAllEntities();

        GameWallet intermissionWallet = new GameWallet(Constants.GoldPerWave);
        intermissionWallet.m_gold += GameHelper.RelicCount<ContentNewInvestmentsRelic>() * m_gameController.m_waveNum * 10;

        player.m_wallet.AddResources(intermissionWallet);

        player.OnEndWave();

        Globals.m_canScroll = true;
        Globals.m_inIntermission = true;
        Globals.m_selectedCard = null;

        Globals.m_selectedEntity = null;

        List<GameCard> exclusionCards = new List<GameCard>();
        GameCard cardOne = GameCardFactory.GetRandomStandardEntityCard();
        exclusionCards.Add(cardOne);
        GameCard cardTwo = GameCardFactory.GetRandomStandardEntityCard(exclusionCards);
        exclusionCards.Add(cardTwo);
        GameCard cardThree = GameCardFactory.GetRandomStandardEntityCard(exclusionCards);

        UICardSelectController.Instance.Init(cardOne, cardTwo, cardThree);
    }

    public void EndIntermission()
    {
        if (Constants.FogOfWar)
        {
            WorldGridManager.Instance.EndIntermissionFogUpdate();
        }

        UITooltipController.Instance.ClearTooltipStack();
        Globals.m_canScroll = true;
        Globals.m_selectedIntermissionBuilding = null;

        Globals.m_inIntermission = false;
        m_hasSpawnedEliteThisWave = false;

        m_gameController.m_player.ResetCurDeck();
        m_gameController.BeginTurnSequence();
    }

    public void WinGame()
    {
        Debug.Log("The player has won!");
        //SceneLoader.ActivateScene("LevelSelectScene", "LevelScene");
    }

    public void FocusNextPlayerEntity()
    {
        List<GameEntity> validEntities = GetValidFocusEntities();
        if (validEntities.Count == 0)
        {
            return;
        }

        m_playerEntityFocusIndex++;

        if (m_playerEntityFocusIndex >= validEntities.Count)
        {
            m_playerEntityFocusIndex = 0;
        }

        UIEntity thisEntity = validEntities[m_playerEntityFocusIndex].m_uiEntity;

        if (Globals.m_selectedEntity != thisEntity)
        {
            UIHelper.SelectEntity(thisEntity);
        }

        UICameraController.Instance.SnapToGameObject(thisEntity.gameObject);
    }

    public void FocusPrevPlayerEntity()
    {
        List<GameEntity> validEntities = GetValidFocusEntities();
        if (validEntities.Count == 0)
        {
            return;
        }

        m_playerEntityFocusIndex--;

        if (m_playerEntityFocusIndex < 0)
        {
            m_playerEntityFocusIndex = validEntities.Count-1;
        }

        UIEntity thisEntity = validEntities[m_playerEntityFocusIndex].m_uiEntity;

        if (Globals.m_selectedEntity != thisEntity)
        {
            UIHelper.SelectEntity(thisEntity);
        }

        UICameraController.Instance.SnapToGameObject(thisEntity.gameObject);
    }

    private List<GameEntity> GetValidFocusEntities()
    {
        List<GameEntity> validFocusEntities = new List<GameEntity>();

        GamePlayer player = GameHelper.GetPlayer();

        //Early exit, empty list
        if (player == null)
        {
            return validFocusEntities;
        }

        List<GameEntity> playerEntities = player.m_controlledEntities;

        //Early exit, empty list
        if (playerEntities.Count == 0)
        {
            return validFocusEntities;
        }

        //Determine which entities in the player list are valid
        for (int i = 0; i < playerEntities.Count; i++)
        {
            if (playerEntities[i].GetCurAP() > 0)
            {
                validFocusEntities.Add(playerEntities[i]);
            }
        }

        return validFocusEntities;
    }

    public bool HasSpawnedEliteThisWave()
    {
        return m_hasSpawnedEliteThisWave;
    }

    public void SetHasSpawnedEliteThisWave(bool newVal)
    {
        m_hasSpawnedEliteThisWave = newVal;
    }

    public bool HasSpawnedBoss()
    {
        return m_hasSpawnedBoss;
    }

    public void SetHasSpawnedBoss(bool newVal)
    {
        m_hasSpawnedBoss = newVal;
    }

    public void OnApplicationQuit()
    {
        if (m_gameController != null)
        {
            GameFiles.ExportPlayerSaveData(m_gameController.GamePlayerSaveData);
        }
    }
}
