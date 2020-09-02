using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

public class WorldController : Singleton<WorldController>
{
    public GameController m_gameController { get; private set; }

    public List<UICard> m_playerHand { get; private set; }

    void Start()
    {
        m_gameController = new GameController();
        m_gameController.LateInit();
        m_playerHand = new List<UICard>();
    }

    void Update()
    {
        HandlePlayerHand();

        if (Input.GetKeyDown(KeyCode.Y))
        {
            SceneLoader.ActivateScene("NickTestScene", "AlexTestScene");
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            SceneLoader.ActivateScene("AlexTestScene", "NickTestScene");
        }
    }

    public void PlayCard(UICard card, WorldElementBase worldElementTarget)
    {
        card.CardPlayed(worldElementTarget);
        Recycler.Recycle<UICard>(card);
        Globals.m_selectedCard = null;

        m_gameController.m_player.PlayCard(card.m_card);

        m_playerHand.Remove(card);
    }

    private void HandlePlayerHand()
    {
        List<GameCard> playerHand = m_gameController.m_player.m_hand;

        for (int i = 0; i < playerHand.Count; i++)
        {
            if (!HasUICardForGameCard(playerHand[i]))
            {
                UICard obj = FactoryManager.Instance.GetFactory<UICardFactory>().CreateObject<UICard>(playerHand[i]);

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

        for (int i = 0; i < m_playerHand.Count; i++)
        {
            Vector3 pos = new Vector3(-10.0f + i * 3.75f, -6.6f, -3.0f);
            m_playerHand[i].gameObject.transform.localPosition = pos;
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
        ClearHand();

        m_gameController.MoveToNextTurn();
    }

    private void ClearHand()
    {
        for (int i = m_playerHand.Count - 1; i >= 0; i--)
        {
            Recycler.Recycle<UICard>(m_playerHand[i]);
            Globals.m_selectedCard = null;
            m_playerHand.RemoveAt(i);
        }
    }


    private void ClearAllEntities()
    {
        //TODO - Remove all entities from the map (ie. at the start of intermission phase)
    }

    public void StartIntermission()
    {
        UITooltipController.Instance.ClearTooltipStack();
        ClearAllEntities();

        GamePlayer player = m_gameController.m_player;

        player.OnEndWave();

        Globals.m_canScroll = true;
        Globals.m_inIntermission = true;
        Globals.m_selectedCard = null;

        Globals.m_selectedEntity = null;

        if (player.m_waveNum == Constants.FinalWaveNum)
        {
            Debug.Log("Player is victorious!");
        }
    }

    public void EndIntermission()
    {
        UITooltipController.Instance.ClearTooltipStack();
        Globals.m_canScroll = true;
        Globals.m_selectedIntermissionBuilding = null;

        Globals.m_inIntermission = false;
    }
}
