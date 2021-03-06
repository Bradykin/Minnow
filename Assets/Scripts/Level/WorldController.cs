﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;
using UnityEngine.SceneManagement;
using System.Linq;

public class WorldController : Singleton<WorldController>
{
    public GameController m_gameController { get; private set; }

    public List<UICard> m_playerHand { get; private set; }

    private GameUnit m_playerUnitFocus;
    private int m_playerUnitFocusIndex;

    public bool m_isInGame;
    public bool m_isEndTurnLocked;

    protected override void Awake()
    {
        base.Awake();
    }

    public void BeginLevel(GameMap map, bool isContinue)
    {
        m_isInGame = true;

        if (isContinue)
        {
            Timer.Instance.SetLevelTime(PlayerDataManager.PlayerAccountData.PlayerRunData.m_runTimer);
        }
        else
        {
            Timer.Instance.ResetLevelTime();
        }
        m_gameController = new GameController(map);
        map.TriggerStartMap();

        m_playerHand = new List<UICard>();

        m_playerUnitFocus = null;
        m_playerUnitFocusIndex = 0;
        Globals.m_selectedCard = null;
    }

    public void EndLevel(RunEndType endType)
    {
        m_isInGame = false;

        m_gameController.EndLevel(endType);
        m_gameController = null;
        for(int i = 0; i < m_playerHand.Count; i++)
        {
            Recycler.Recycle<UICard>(m_playerHand[i]);
        }
        m_playerHand = null;

        m_playerUnitFocusIndex = 0;
        Globals.m_selectedCard = null;
    }

    void Update()
    {
        if (!m_isInGame)
        {
            return;
        }
        
        HandlePlayerHand();

        if (UIHelper.GetKeyDown(KeyCode.Q))
        {
            if (GameHelper.IsPlayerTurn())
            {
                FocusPrevPlayerUnit();
            }
        }

        if (UIHelper.GetKeyDown(KeyCode.E))
        {
            if (GameHelper.IsPlayerTurn())
            {
                FocusNextPlayerUnit();
            }
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
        if (GameHelper.IsInGame())
        {
            WorldController.Instance.UpdateHand();
        }
    }

    private void HandlePlayerHand()
    {
        if (!GameHelper.IsInGame())
        {
            return;
        }

        List<GameCard> playerHand = m_gameController.m_player.m_hand;

        for (int i = 0; i < playerHand.Count; i++)
        {
            if (!HasUICardForGameCard(playerHand[i]))
            {
                UICard obj = FactoryManager.Instance.GetFactory<UICardFactory>().CreateObject<UICard>(playerHand[i], UICard.CardDisplayType.Hand);

                m_playerHand.Add(obj);
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

        float xPosBase = -400.0f;
        float xPosMult = (140.0f - (Mathf.Max((m_playerHand.Count - Constants.InitialHandSize), 0) * 5f));
        float yPos = -320.0f;
        float yPosBigOffset = 125.0f;
        float zPos = 0.0f;
        float smallCardScale = 0.8f;
        float bigCardScale = 1.25f;
        float splitDif = 0f;

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

    private void ClearAllUnits()
    {
        for (int i = 0; i < WorldGridManager.Instance.m_gridArray.Length; i++)
        {
            if (WorldGridManager.Instance.m_gridArray[i].GetGameTile().IsOccupied() && !(WorldGridManager.Instance.m_gridArray[i].GetGameTile().GetOccupyingUnit() is ContentRoyalCaravan))
            {
                WorldGridManager.Instance.m_gridArray[i].GetGameTile().GetOccupyingUnit().SetGameTile(null);
                WorldGridManager.Instance.m_gridArray[i].RecycleUnit(null);
            }
        }

        m_gameController.m_gameOpponent.m_controlledUnits.Clear();

        if (m_gameController.m_player.m_controlledUnits.Any(u => u.GetType() == typeof(ContentRoyalCaravan)))
        {
            for (int i = m_gameController.m_player.m_controlledUnits.Count - 1; i >= 0; i--)
            {
                if (m_gameController.m_player.m_controlledUnits[i].GetType() == typeof(ContentRoyalCaravan))
                {
                    m_gameController.m_player.m_controlledUnits[i].OnEndWave();
                    continue;
                }

                m_gameController.m_player.m_controlledUnits.RemoveAt(i);
            }
        }
        else
        {
            m_gameController.m_player.m_controlledUnits.Clear();
        }
    }

    public void StartIntermission()
    {
        m_gameController.m_curRage = 0;

        GamePlayer player = m_gameController.m_player;

        if (!Globals.loadingRun)
        {
            player.m_spellsPlayedPreviousTurn = 0;

            UITooltipController.Instance.ClearTooltipStack();
            ClearAllUnits();

            UIWorldElementNotificationController.Instance.ClearAllWorldElementNotifications();

            GameWallet intermissionWallet = new GameWallet(Constants.GoldPerWave);
            if (GameHelper.HasRelic<ContentNewInvestmentsRelic>())
            {
                intermissionWallet.m_gold += 35;
                UIHelper.TriggerRelicAnimation<ContentNewInvestmentsRelic>();
            }
            if (GameHelper.HasRelic<ContentHarvestOfTelumRelic>())
            {
                intermissionWallet.m_gold += 25;
                UIHelper.TriggerRelicAnimation<ContentHarvestOfTelumRelic>();
            }

            if (intermissionWallet.m_gold > 0)
            {
                player.GainGold(intermissionWallet.m_gold);
            }

            player.OnEndWave();

            if (PlayerDataManager.PlayerAccountData.m_altarsUnlockedOnAccount)
            {
                if (m_gameController.m_currentWaveNumber == Constants.AltarWave)
                {
                    WorldGridManager.Instance.PlaceAltars();
                    UIHelper.CreateHUDNotification("Altars Rising", "Altars to great gods of the region have risen. Once you claim one, that god will declare you their champion!");
                }
            }

            m_gameController.GetCurMap().TriggerMapEvents(m_gameController.m_currentWaveNumber, ScheduledActionTime.StartIntermission);
        }

        Globals.m_canScroll = true;
        m_gameController.m_runStateType = RunStateType.Intermission;
        Globals.m_selectedCard = null;

        Globals.m_selectedUnit = null;

        UIIntermissionController.Instance.StartSelectionMenus();
    }

    public void EndIntermission()
    {
        m_gameController.m_savedInIntermission = false;

        if (Constants.FogOfWar)
        {
            WorldGridManager.Instance.EndIntermissionFogUpdate();
        }

        UITooltipController.Instance.ClearTooltipStack();
        Globals.m_canScroll = true;
        Globals.m_selectedIntermissionBuilding = null;

        m_gameController.m_runStateType = RunStateType.Gameplay;

        m_gameController.m_player.ResetCurDeck();
        m_gameController.BeginTurnSequence();

    }

    public void FocusNextPlayerUnit()
    {
        List<GameUnit> validUnits = GetValidFocusUnits();
        if (validUnits.Count == 0)
        {
            return;
        }

        if (validUnits.Contains(m_playerUnitFocus))
        {
            m_playerUnitFocusIndex = validUnits.IndexOf(m_playerUnitFocus);
        }
        else
        {
            m_playerUnitFocusIndex = Mathf.Clamp(m_playerUnitFocusIndex, 0, validUnits.Count - 1);
        }

        m_playerUnitFocusIndex++;

        if (m_playerUnitFocusIndex >= validUnits.Count)
        {
            m_playerUnitFocusIndex = 0;
        }

        WorldUnit thisUnit = validUnits[m_playerUnitFocusIndex].m_worldUnit;

        if (Globals.m_selectedUnit != thisUnit)
        {
            UIHelper.SelectUnit(thisUnit);
        }

        UICameraController.Instance.SnapToGameObject(thisUnit.gameObject);
    }

    public void FocusPrevPlayerUnit()
    {
        List<GameUnit> validUnits = GetValidFocusUnits();
        if (validUnits.Count == 0)
        {
            return;
        }

        if (validUnits.Contains(m_playerUnitFocus))
        {
            m_playerUnitFocusIndex = validUnits.IndexOf(m_playerUnitFocus);
        }
        else
        {
            m_playerUnitFocusIndex = Mathf.Clamp(m_playerUnitFocusIndex, 0, validUnits.Count - 1);
        }

        m_playerUnitFocusIndex--;

        if (m_playerUnitFocusIndex < 0)
        {
            m_playerUnitFocusIndex = validUnits.Count-1;
        }

        WorldUnit thisUnit = validUnits[m_playerUnitFocusIndex].m_worldUnit;

        if (Globals.m_selectedUnit != thisUnit)
        {
            UIHelper.SelectUnit(thisUnit);
        }

        UICameraController.Instance.SnapToGameObject(thisUnit.gameObject);
    }

    private List<GameUnit> GetValidFocusUnits()
    {
        List<GameUnit> validFocusUnits = new List<GameUnit>();

        GamePlayer player = GameHelper.GetPlayer();

        //Early exit, empty list
        if (player == null)
        {
            return validFocusUnits;
        }

        List<GameUnit> playerUnits = player.m_controlledUnits;

        //Early exit, empty list
        if (playerUnits.Count == 0)
        {
            return validFocusUnits;
        }

        //Determine which units in the player list are valid
        for (int i = 0; i < playerUnits.Count; i++)
        {
            if (playerUnits[i].GetCurStamina() > 0)
            {
                validFocusUnits.Add(playerUnits[i]);
            }
        }

        return validFocusUnits;
    }

    public void OnApplicationQuit()
    {
        Files.ExportPlayerAccountData(PlayerDataManager.PlayerAccountData);
    }
}
