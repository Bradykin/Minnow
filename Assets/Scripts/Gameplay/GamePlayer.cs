﻿
using Game.Util;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GamePlayer : ITurns
{
    public static GameCard StarterSimpleUnit = new ContentDwarvenSoldierCard();
    public static GameCard StarterAdvancedUnit = new ContentStoneGolemCard();
    public static GameCard StarterDamageSpell = new ContentFireboltCard();
    public static GameCard StarterDefensiveSpell = new ContentAegisCard();
    public static GameCard StarterExileSpell = new ContentGrowTalonsCard();
    public static GameRelic StarterRelic = new ContentLivingStoneRelic();

    public int m_curEnergy;
    private int m_maxEnergy;

    public GameWallet m_wallet;

    public GameDeck m_deckBase { get; private set; }
    public GameDeck m_curDeck { get; private set; }

    public List<GameCard> m_hand { get; private set; }

    public List<GameUnit> m_controlledUnits { get; private set; }
    public List<GameBuildingBase> m_controlledBuildings { get; private set; }

    private int m_spellPower = 0;
    public ContentCastleBuilding Castle => (ContentCastleBuilding)m_controlledBuildings.FirstOrDefault(b => b is ContentCastleBuilding);

    private GameRelicHolder m_relics;

    private int m_curActions;
    private int m_maxActions;

    public List<GameCard> m_cardsToDiscard = new List<GameCard>();
    public List<GameCard> m_cardsInExile = new List<GameCard>();

    private List<GamePlayerScheduledActions> m_scheduledActions = new List<GamePlayerScheduledActions>();

    public GamePlayer()
    {
        m_hand = new List<GameCard>();
        m_controlledUnits = new List<GameUnit>();
        m_controlledBuildings = new List<GameBuildingBase>();
        m_relics = new GameRelicHolder();
        m_wallet = new GameWallet(25);

        m_maxActions = Constants.StartingActions;

        m_deckBase = new GameDeck();
        m_curDeck = new GameDeck();
        m_cardsInExile = new List<GameCard>();
    }

    public void LateInit()
    {
        m_deckBase.FillStartingDeck();

        m_maxEnergy = Constants.StartingEnergy;
        m_curEnergy = GetMaxEnergy();

        ResetCurDeck();

        m_curDeck.Shuffle();

        AddRelic(GameRelicFactory.GetGameRelicClone(StarterRelic));
    }

    public void DrawHand()
    {
        DiscardHand();

        int handSize = GetDrawHandSize();
        for (int i = 0; i < handSize; i++)
        {
            DrawCard(false);
        }
    }

    public void DiscardHand()
    {
        WorldController.Instance.ClearHand();
        for (int i = 0; i < m_hand.Count; i++)
        {
            m_curDeck.AddToDiscard(m_hand[i]);
        }
        m_hand = new List<GameCard>();
    }

    public void DrawCard(bool triggerKnowledgeable = true)
    {
        GameCard card = m_curDeck.DrawCard();

        if (card != null) //This can be null if the deck and discard are both empty
        {
            card.OnDraw();

            if (triggerKnowledgeable)
            {
                TriggerKnowledgeable();
            }

            if (m_hand.Count >= Constants.MaxHandSize)
            {
                m_curDeck.AddToDiscard(card);
            }
            else
            {
                m_hand.Add(card);
            }
        }
    }

    public void TriggerKnowledgeable()
    {
        for (int i = 0; i < m_controlledUnits.Count; i++)
        {
            m_controlledUnits[i].TriggerKnowledgeable();
        }
    }

    public void DrawCards(int toDraw, bool triggerKnowledgeable = true)
    {
        for (int i = 0; i < toDraw; i++)
        {
            DrawCard(triggerKnowledgeable);
        }
    }

    public void PlayCard(GameCard card)
    {
        if (card.m_shouldExile)
        {
            m_cardsInExile.Add(card);
            m_curDeck.RemoveCard(card);
        }
        else
        {
            m_cardsToDiscard.Add(card);
        }

        m_hand.Remove(card);
    }

    public void SpendEnergy(int toSpend)
    {
        m_curEnergy -= toSpend;
        if (m_curEnergy < 0)
        {
            Debug.LogWarning("Somehow spent below 0 energy.");
            m_curEnergy = 0;
        }
    }

    public void SpendActions(int toSpend)
    {
        m_curActions -= toSpend;
        if (m_curActions < 0)
        {
            Debug.LogWarning("Somehow spent below 0 actions.");
            m_curActions = 0;
        }
    }

    //Bonus actions can go above the max (from things like smithies)
    public void AddBonusActions(int toAdd)
    {
        m_curActions += toAdd;
    }

    public void ResetCurDeck()
    {
        for (int i = 0; i < m_hand.Count; i++)
        {
            m_curDeck.AddToDiscard(m_hand[i]);
        }

        m_hand = new List<GameCard>();

        m_curDeck = new GameDeck();

        for (int i = 0; i < m_deckBase.Count(); i++)
        {
            m_curDeck.AddCard(m_deckBase.GetCardByIndex(i));
        }

        for (int i = 0; i < m_curDeck.Count(); i++)
        {
            m_curDeck.GetCardByIndex(i).ResetCard();
        }

        m_curDeck.Shuffle();
    }

    public void AddScheduledAction(ScheduledActionTime actionTime, GameAction action)
    {
        GamePlayerScheduledActions scheduledAction = new GamePlayerScheduledActions
        {
            scheduledActionTime = actionTime,
            gameAction = action
        };

        m_scheduledActions.Add(scheduledAction);
    }

    public void AddEnergy(int toAdd)
    {
        m_curEnergy += toAdd;
    }

    public int GetSpellPower()
    {
        int toReturn = m_spellPower;

        for (int i = 0; i < m_controlledBuildings.Count; i++)
        {
            if (m_controlledBuildings[i] is ContentMagicSchoolBuilding && !m_controlledBuildings[i].m_isDestroyed)
            {
                toReturn += ((ContentMagicSchoolBuilding)m_controlledBuildings[i]).m_magicIncrease;
            }
        }

        toReturn += 3 * GameHelper.RelicCount<ContentDominerickRefrainRelic>();
        toReturn -= 3 * GameHelper.RelicCount<ContentTomeOfDuluhainRelic>();
        toReturn += Globals.m_tempSpellpower;

        return toReturn;
    }

    public void AddControlledUnit(GameUnit toAdd)
    {
        m_controlledUnits.Add(toAdd);
    }

    public void AddControlledBuilding(GameBuildingBase toAdd)
    {
        m_controlledBuildings.Add(toAdd);
    }

    public void RemoveControlledUnit(GameUnit toRemove)
    {
        m_controlledUnits.Remove(toRemove);
    }

    public void RemoveControlledBuilding(GameBuildingBase toRemove)
    {
        m_controlledBuildings.Remove(toRemove);
    }

    public void InformWasSummoned(GameUnit summonedUnit)
    {
        for (int i = 0; i < m_controlledUnits.Count; i++)
        {
            if (m_controlledUnits[i] == summonedUnit)
            {
                continue;
            }

            m_controlledUnits[i].OnOtherSummon(summonedUnit);
        }
    }

    public void AddCardToHand(GameCard card, bool addToDeckPermanent)
    {
        if (m_hand.Count >= Constants.MaxHandSize)
        {
            Debug.Log("Add a card to discard for being past max hand size");
            AddCardToDiscard(card, addToDeckPermanent);
            return;
        }

        m_hand.Add(card);

        if (addToDeckPermanent)
        {
            m_deckBase.AddCard(card);
        }
    }

    public void AddCardToDiscard(GameCard card, bool addToDeckPermanent)
    {
        m_curDeck.AddToDiscard(card);

        if (addToDeckPermanent)
        {
            m_deckBase.AddCard(card);
        }
    }

    public GameRelicHolder GetRelics()
    {
        return m_relics;
    }

    public void AddRelic(GameRelic toAdd)
    {
        if (toAdd is ContentLoadedChestRelic)
        {
            m_wallet.AddResources(new GameWallet(75 * (1 + toAdd.GetRelicLevel())));
        }

        if (toAdd is ContentTotemOfTheWolfRelic && GameHelper.RelicCount<ContentTotemOfTheWolfRelic>() == 0 && WorldController.Instance.m_gameController.m_inTurns && 
            WorldController.Instance.m_gameController.m_currentWaveTurn <= WorldController.Instance.m_gameController.GetEndWaveTurn())
        {
            Globals.m_totemOfTheWolfTurn = Random.Range(WorldController.Instance.m_gameController.m_currentWaveTurn + 1, WorldController.Instance.m_gameController.GetEndWaveTurn() + 1);
            Debug.Log("Set wolf turn to " + Globals.m_totemOfTheWolfTurn);
        }

        m_relics.AddRelic(toAdd);

        WorldController.Instance.UpdateHand();
    }

    public int GetMaxEnergy()
    {
        int toReturn = m_maxEnergy;

        for (int i = 0; i < m_controlledBuildings.Count; i++)
        {
            if (m_controlledBuildings[i] is ContentTempleBuilding)
            {
                toReturn += 1;
            }
        }

        toReturn += (new ContentOrbOfEnergyRelic().GetRelicLevel() + 1) * GameHelper.RelicCount<ContentOrbOfEnergyRelic>();

        return toReturn;
    }

    private int GetDrawHandSize()
    {
        int toReturn = Constants.InitialHandSize;

        if (GameHelper.GetGameController().m_currentWaveTurn == 0)
        {
            toReturn += 3 * GameHelper.RelicCount<ContentSackOfManyShapesRelic>();
        }

        toReturn += (new ContentMaskOfAgesRelic().GetRelicLevel() + 1) * GameHelper.RelicCount<ContentMaskOfAgesRelic>();

        toReturn += 2 * GameHelper.RelicCount<ContentMysticRuneRelic>();

        toReturn -= 1 * GameHelper.RelicCount<ContentPinnacleOfFearRelic>();

        for (int i = 0; i < m_controlledBuildings.Count; i++)
        {
            if (m_controlledBuildings[i] is ContentMineBuilding && !m_controlledBuildings[i].m_isDestroyed)
            {
                toReturn += 1;
            }
        }

        return toReturn;
    }

    public bool HasUnitsThatWillOvercapStamina()
    {
        for (int i = 0; i < m_controlledUnits.Count; i++)
        {
            if (m_controlledUnits[i].GetCurStamina() + m_controlledUnits[i].GetStaminaRegen() > m_controlledUnits[i].GetMaxStamina())
            {
                return true;
            }
        }

        return false;
    }

    public bool CanPlayAnythingInHand()
    {
        for (int i = 0; i < m_hand.Count; i++)
        {
            if (m_hand[i].GetCost() <= m_curEnergy)
            {
                return true;
            }
        }

        return false;
    }

    public int GetCurActions()
    {
        return m_curActions;
    }

    public int GetMaxActions()
    {
        int toReturn = m_maxActions;

        toReturn += 1 * GameHelper.RelicCount<ContentHoovesOfProductionRelic>();

        return toReturn;
    }

    public void ResetActions()
    {
        m_curActions = GetMaxActions();
    }

    public void OnEndWave()
    {
        ResetActions();

        m_cardsInExile.Clear();

        for (int i = 0; i < m_controlledBuildings.Count; i++)
        {
            m_controlledBuildings[i].TriggerEndOfWave();
        }

        for (int i = m_scheduledActions.Count - 1; i >= 0; i--)
        {
            if (m_scheduledActions[i].scheduledActionTime == ScheduledActionTime.EndOfWave)
            {
                m_scheduledActions[i].gameAction.DoAction();
                m_scheduledActions.RemoveAt(i);
                continue;
            }

            if (m_scheduledActions[i].scheduledActionTime == ScheduledActionTime.StartOfTurn)
            {
                m_scheduledActions.RemoveAt(i);
                continue;
            }

            if (m_scheduledActions[i].scheduledActionTime == ScheduledActionTime.EndOfTurn)
            {
                m_scheduledActions.RemoveAt(i);
                continue;
            }
        }

        for (int i = 0; i < m_deckBase.GetDeck().Count; i++)
        {
            m_deckBase.GetDeck()[i].SetTempCost(0);
        }
    }

    public void TriggerSpellcraft(GameCard.Target targetType, GameTile targetTile)
    {
        for (int i = 0; i < m_controlledUnits.Count; i++)
        {
            m_controlledUnits[i].SpellCast(targetType, targetTile);
        }
    }

    //============================================================================================================//

    public void StartTurn()
    {
        DrawHand();

        if (Castle != null && (Constants.SnapToCastleAtStart || GameHelper.GetGameController().m_currentWaveTurn == 0))
        {
            UICameraController.Instance.SnapToGameObject(Castle.GetWorldTile().gameObject);
        }
        m_curEnergy = GetMaxEnergy();

        if (GameHelper.GetGameController().m_currentWaveTurn == 0)
        {
            AddEnergy(2 * GameHelper.RelicCount<ContentSackOfManyShapesRelic>());
        }

        for (int i = 0; i < m_controlledUnits.Count; i++)
        {
            m_controlledUnits[i].StartTurn();
        }

        for (int i = 0; i < m_controlledBuildings.Count; i++)
        {
            m_controlledBuildings[i].StartTurn();
        }

        for (int i = m_scheduledActions.Count - 1; i >= 0; i--)
        {
            if (m_scheduledActions[i].scheduledActionTime == ScheduledActionTime.StartOfTurn)
            {
                m_scheduledActions[i].gameAction.DoAction();
                m_scheduledActions.RemoveAt(i);
            }
        }
        Globals.m_canSelect = true;
    }

    public void EndTurn()
    {
        for (int i = 0; i < m_cardsToDiscard.Count; i++)
        {
            m_curDeck.AddToDiscard(m_cardsToDiscard[i]);
        }

        m_cardsToDiscard = new List<GameCard>();

        for (int i = 0; i < m_controlledUnits.Count; i++)
        {
            m_controlledUnits[i].EndTurn();
        }

        for (int i = 0; i < m_controlledBuildings.Count; i++)
        {
            m_controlledBuildings[i].EndTurn();
        }

        for (int i = m_scheduledActions.Count - 1; i >= 0; i--)
        {
            if (m_scheduledActions[i].scheduledActionTime == ScheduledActionTime.EndOfTurn)
            {
                m_scheduledActions[i].gameAction.DoAction();
                m_scheduledActions.RemoveAt(i);
            }
        }

        Globals.m_spellsPlayedPreviousTurn = Globals.m_spellsPlayedThisTurn;
        Globals.m_spellsPlayedThisTurn = 0;
        Globals.m_fletchingCount = 0;
        Globals.m_goldPerShivKill = 0;
        Globals.m_tempSpellpower = 0;
        Globals.m_canSelect = false;
    }
}
