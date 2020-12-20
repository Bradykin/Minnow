using Game.Util;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GamePlayer : ITurns, ISave<JsonGamePlayerData>, ILoad<JsonGamePlayerData>
{
    public int m_curEnergy;
    private int m_maxEnergy;

    private GameWallet m_wallet;

    public GameDeck m_deckBase { get; private set; }
    public GameDeck m_curDeck { get; private set; }

    public List<GameCard> m_hand { get; private set; }

    public List<GameUnit> m_controlledUnits { get; private set; }
    public List<GameBuildingBase> m_controlledBuildings { get; private set; }

    private int m_magicPower = 0;

    public bool IsUnitCastle = false;

    private GameRelicHolder m_relics;

    private int m_curActions;
    private int m_maxActions;

    public List<GameCard> m_cardsToDiscard = new List<GameCard>();
    public List<GameCard> m_cardsInExile = new List<GameCard>();

    private List<GamePlayerScheduledActions> m_scheduledActions = new List<GamePlayerScheduledActions>();

    //Specific variables tracked for various spell effects
    public int m_spellsPlayedPreviousTurn = 0;
    public int m_spellsPlayedThisTurn = 0;
    public int m_fletchingPowerIncrease = 0;
    public int m_tempMagicPowerIncrease = 0;
    public int m_totemOfTheWolfTurn = -1;

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

    public GameElementBase GetCastleGameElement()
    {
        GameBuildingBase CastleBuilding = m_controlledBuildings.FirstOrDefault(b => b is ContentCastleBuilding);

        if (CastleBuilding != null)
        {
            return CastleBuilding;
        }

        GameUnit CastleUnit = m_controlledUnits.FirstOrDefault(u => u is ContentRoyalCaravan);

        if (CastleUnit != null)
        {
            return CastleUnit;
        }

        return null;
    }

    public GameTile GetCastleGameTile()
    {
        GameBuildingBase CastleBuilding = m_controlledBuildings.FirstOrDefault(b => b is ContentCastleBuilding);

        if (CastleBuilding != null)
        {
            return CastleBuilding.GetGameTile();
        }

        GameUnit CastleUnit = m_controlledUnits.FirstOrDefault(u => u is ContentRoyalCaravan);

        if (CastleUnit != null)
        {
            return CastleUnit.GetGameTile();
        }

        return null;
    }

    public WorldTile GetCastleWorldTile()
    {
        GameTile castleTile = GetCastleGameTile();
        if (castleTile == null)
        {
            return null;
        }

        return castleTile.GetWorldTile();
    }

    public void LateInit()
    {
        if (Globals.loadingRun)
        {
            return;
        }

        m_maxEnergy = Constants.StartingEnergy;
        m_curEnergy = GetMaxEnergy();

        m_deckBase.FillStartingDeck();
        ResetCurDeck();

        m_curDeck.Shuffle();

        AddRelic(GameRelicFactory.GetRelicByName(PlayerDataManager.PlayerAccountData.StarterRelicName));
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
            //This block is what happens when you can actually draw a card
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

            if (GameHelper.HasRelic<ContentAncientMysteryRelic>())
            {
                m_controlledUnits[i].TriggerKnowledgeable();
            }
        }
    }

    public void DrawCards(int toDraw, bool triggerKnowledgeable = true)
    {
        for (int i = 0; i < toDraw; i++)
        {
            DrawCard(triggerKnowledgeable);
        }
    }

    public void PlayCard(GameCard card, bool removeFromHand = true)
    {
        bool shouldExile = card.m_shouldExile;

        if (GameHelper.HasRelic<ContentGoldenKnotRelic>() && card is GameCardSpellBase)
        {
            shouldExile = false;
        }

        if (shouldExile)
        {
            m_cardsInExile.Add(card);
            m_curDeck.RemoveCard(card);
        }
        else
        {
            m_cardsToDiscard.Add(card);
        }

        if (removeFromHand)
        {
            RemoveCardFromHand(card);
        }
    }

    public void RemoveCardFromHand(GameCard toRemove)
    {
        m_hand.Remove(toRemove);

        if (m_hand.Count == 0)
        {
            if (GetRelics().GetNumRelics<ContentMaskOfSpeedRelic>() > 0)
            {
                DrawCard();
            }
        }
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

    public int GetMagicPower()
    {
        int toReturn = m_magicPower;

        for (int i = 0; i < m_controlledBuildings.Count; i++)
        {
            if (m_controlledBuildings[i] is ContentMagicSchoolBuilding && !m_controlledBuildings[i].m_isDestroyed)
            {
                toReturn += ((ContentMagicSchoolBuilding)m_controlledBuildings[i]).m_magicIncrease;
            }
        }

        if (GameHelper.HasRelic<ContentDominerickRefrainRelic>())
        {
            toReturn += 3;
        }
        if (GameHelper.HasRelic<ContentTomeOfDuluhainRelic>())
        {
            toReturn -= 3;
        }
        toReturn += m_tempMagicPowerIncrease;

        return toReturn;
    }

    public void AddMagicPower(int toAdd)
    {
        m_magicPower += toAdd;
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

    public void InformHasMoved(GameUnit movedUnit, GameTile startingTile, GameTile endingTile, List<GameTile> pathBetweenTiles)
    {
        for (int i = 0; i < m_controlledUnits.Count; i++)
        {
            if (m_controlledUnits[i] == movedUnit)
            {
                continue;
            }

            m_controlledUnits[i].OnOtherMove(movedUnit, startingTile, endingTile, pathBetweenTiles);
        }
    }

    public void InformHasDied(GameUnit deadUnit, GameTile deathLocation)
    {
        for (int i = 0; i < m_controlledUnits.Count; i++)
        {
            if (m_controlledUnits[i] == deadUnit)
            {
                continue;
            }

            m_controlledUnits[i].OnOtherDie(deadUnit, deathLocation);
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
            GainGold(75);
        }

        if (toAdd is ContentGreedOfDorphinRelic)
        {
            GainGold(200);
        }

        if (toAdd is ContentEyeOfMoragRelic)
        {
            for (int i = 0; i < WorldGridManager.Instance.m_gridArray.Length; i++)
            {
                GameTile gameTile = WorldGridManager.Instance.m_gridArray[i].GetGameTile();
                if (gameTile.GetTerrain().IsForest() && gameTile.GetTerrain().CanBurn())
                {
                    gameTile.SetTerrain(GameTerrainFactory.GetBurnedTerrainClone(gameTile.GetTerrain()));
                    if (gameTile.IsOccupied())
                    {
                        gameTile.GetOccupyingUnit().Die();
                    }
                }
            }
        }

        if (toAdd is ContentEyeOfTelsimirRelic)
        {
            for (int i = 0; i < WorldGridManager.Instance.m_gridArray.Length; i++)
            {
                GameTile gameTile = WorldGridManager.Instance.m_gridArray[i].GetGameTile();
                if (gameTile.GetTerrain().IsWater())
                {
                    gameTile.SetTerrain(new ContentIceTerrain(), true);
                }
            }
        }

        if (toAdd is ContentEyeOfDorosonRelic)
        {
            for (int i = 0; i < WorldGridManager.Instance.m_gridArray.Length; i++)
            {
                GameTile gameTile = WorldGridManager.Instance.m_gridArray[i].GetGameTile();
                if (gameTile.GetTerrain().IsMountain())
                {
                    gameTile.SetTerrain(new ContentDirtPlainsTerrain(), true);
                }
            }
        }

        if (toAdd is ContentAncientEvilRelic)
        {
            GainGold(1);
        }

        m_relics.AddRelic(toAdd);

        if (toAdd is ContentTheGreatestGiftRelic)
        {
            for (int i = 0; i < m_controlledBuildings.Count; i++)
            {
                m_controlledBuildings[i].GetWorldTile().ClearSurroundingFog(m_controlledBuildings[i].GetSightRange());
            }

            for (int i = 0; i < m_controlledUnits.Count; i++)
            {
                m_controlledUnits[i].GetWorldTile().ClearSurroundingFog(m_controlledUnits[i].GetSightRange());
            }
        }

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

        if (GameHelper.HasRelic<ContentTacticsOfMonRelic>())
        {
            toReturn += 2;
        }

        if (GameHelper.HasRelic<ContentOrbOfEnergyRelic>())
        {
            toReturn += 1;
        }

        return toReturn;
    }

    public int GetCurEnergy()
    {
        return m_curEnergy;
    }

    private int GetDrawHandSize()
    {
        int toReturn = Constants.InitialHandSize;

        if (GameHelper.GetGameController().m_currentTurnNumber == 1)
        {
            if (GameHelper.HasRelic<ContentSackOfManyShapesRelic>())
            {
                toReturn += 3;
            }
        }

        if (GameHelper.HasRelic<ContentAncientRitualRelic>())
        {
            toReturn -= 4;
        }

        if (GameHelper.HasRelic<ContentMaskOfAgesRelic>())
        {
            toReturn += 1;
        }

        if (GameHelper.HasRelic<ContentTacticsOfMonRelic>())
        {
            toReturn += 2;
        }

        if (GameHelper.HasRelic<ContentMysticRuneRelic>())
        {
            toReturn += 2;
        }

        if (GameHelper.HasRelic<ContentPinnacleOfFearRelic>())
        {
            toReturn -= 1;
        }

        for (int i = 0; i < m_controlledBuildings.Count; i++)
        {
            if (m_controlledBuildings[i] is ContentMineBuilding && !m_controlledBuildings[i].m_isDestroyed)
            {
                toReturn += 1;
            }
        }

        //Minimize it to 1
        if (toReturn <= 0)
        {
            toReturn = 1;
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

        if (GameHelper.HasRelic<ContentHoovesOfProductionRelic>())
        {
            toReturn += 1;
        }

        if (GameHelper.HasRelic<ContentEyeOfTelloRelic>())
        {
            toReturn += 4;
        }

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
        m_cardsToDiscard.Clear();

        for (int i = 0; i < m_controlledUnits.Count; i++)
        {
            if (m_controlledUnits[i].GetBleedKeyword() != null)
            {
                m_controlledUnits[i].SubtractKeyword(m_controlledUnits[i].GetBleedKeyword());
            }
        }

        for (int i = 0; i < m_deckBase.Count(); i++)
        {
            if (m_deckBase.GetCardByIndex(i) is GameUnitCard)
            {
                ((GameUnitCard)m_deckBase.GetCardByIndex(i)).m_unit.EndWave();
            }
        }

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

        m_tempMagicPowerIncrease = 0;
    }

    public void OnBeginWave()
    {
        
    }

    public void SpendGold(int toSpend)
    {
        m_wallet.SpendGold(toSpend);
        AudioHelper.PlaySFX(AudioHelper.GoldSpend);
    }

    public void GainGold(int toGain, bool showUINotification = true)
    {
        m_wallet.AddGold(toGain, showUINotification);

        if (toGain >= 75)
        {
            AudioHelper.PlaySFX(AudioHelper.GoldPickupLarge);
        }
        else if (toGain >= 25)
        {
            AudioHelper.PlaySFX(AudioHelper.GoldPickupMedium);
        }
        else
        {
            AudioHelper.PlaySFX(AudioHelper.GoldPickupSmall);
        }
    }

    public int GetGold()
    {
        return m_wallet.m_gold;
    }

    public void TriggerSpellcraft(GameCard.Target targetType, GameTile targetTile)
    {
        for (int i = 0; i < m_controlledUnits.Count; i++)
        {
            m_controlledUnits[i].SpellCast(targetType, targetTile);

            if (m_controlledUnits[i].GetSpellcraftKeyword() != null)
            {
                if (GameHelper.HasRelic<ContentLastHopeRelic>())
                {
                    DrawCard();
                }

                if (GameHelper.HasRelic<ContentProclamationOfSurrenderRelic>())
                {
                    AddEnergy(1);
                }
            }
        }
    }

    public int GetXValue()
    {
        return m_curEnergy;
    }

    //============================================================================================================//

    public void StartTurn()
    {
        if (GameHelper.GetGameController().m_currentTurnNumber >= 5)
        {
            Globals.m_curRage+=2;
        }

        DrawHand();

        if (GetCastleGameElement() != null && (Constants.SnapToCastleAtStart || GameHelper.GetGameController().m_currentTurnNumber == 0))
        {
            UICameraController.Instance.SnapToGameObject(GetCastleWorldTile().gameObject);
        }
        m_curEnergy = GetMaxEnergy();

        if (GameHelper.GetGameController().m_currentTurnNumber == 1)
        {
            if (GameHelper.HasRelic<ContentSackOfManyShapesRelic>())
            {
                AddEnergy(2);
            }
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

        GameHelper.GetGameController().m_randomSeed = (int)System.DateTime.Now.Ticks;
        Random.InitState(GameHelper.GetGameController().m_randomSeed);
        PlayerDataManager.PlayerAccountData.SaveRunData();
        GameNotificationManager.SaveGameDirectorData();
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

        m_spellsPlayedPreviousTurn = m_spellsPlayedThisTurn;
        m_spellsPlayedThisTurn = 0;
        m_fletchingPowerIncrease = 0;
        Globals.m_canSelect = false;
    }

    //============================================================================================================//

    public JsonGamePlayerData SaveToJson()
    {
        JsonGamePlayerData jsonData = new JsonGamePlayerData
        {
            maxEnergy = m_maxEnergy,
            curEnergy = m_curEnergy,
            maxActions = m_maxActions,
            curActions = m_curActions,
            magicPower = m_magicPower,
            jsonDeckBaseData = m_deckBase.SaveToJson(),
            jsonDeckCurrentData = m_curDeck.SaveToJson(),
            jsonGameCardsInHandData = new List<JsonGameCardData>(),
            jsonGameCardsInDiscardData = new List<JsonGameCardData>(),
            jsonGameCardsInExileData = new List<JsonGameCardData>(),
            jsonGameScheduledActionData = new List<JsonGameScheduledActionData>(),
            jsonGameRelicHolderData = m_relics.SaveToJson(),

            goldAmount = m_wallet.m_gold,

            spellsPlayedPreviousTurn = m_spellsPlayedPreviousTurn,
            spellsPlayedThisTurn = m_spellsPlayedThisTurn,
            fletchingPowerIncrease = m_fletchingPowerIncrease,
            tempMagicPowerIncrease = m_tempMagicPowerIncrease,
            totemOfTheWolfTurn = m_totemOfTheWolfTurn
        };

        for (int i = 0; i < m_hand.Count; i++)
        {
            jsonData.jsonGameCardsInHandData.Add(m_hand[i].SaveToJson());
        }

        for (int i = 0; i < m_cardsToDiscard.Count; i++)
        {
            jsonData.jsonGameCardsInDiscardData.Add(m_cardsToDiscard[i].SaveToJson());
        }

        for (int i = 0; i < m_cardsInExile.Count; i++)
        {
            jsonData.jsonGameCardsInExileData.Add(m_cardsInExile[i].SaveToJson());
        }

        for (int i = 0; i < m_scheduledActions.Count; i++)
        {
            jsonData.jsonGameScheduledActionData.Add(new JsonGameScheduledActionData
            {
                scheduledActionTime = (int)m_scheduledActions[i].scheduledActionTime,
                jsonActionData = m_scheduledActions[i].gameAction.SaveToJson(),
                jsonTargetUnitData = m_scheduledActions[i].gameAction.GetGameUnit().SaveToJson()
            });
        }

        return jsonData;
    }

    public void LoadFromJson(JsonGamePlayerData jsonData)
    {
        m_maxEnergy = jsonData.maxEnergy;
        m_curEnergy = jsonData.curEnergy;
        m_maxActions = jsonData.maxActions;
        m_curActions = jsonData.curActions;
        m_magicPower = jsonData.magicPower;

        m_deckBase.LoadFromJson(jsonData.jsonDeckBaseData);
        m_curDeck.LoadFromJson(jsonData.jsonDeckCurrentData);
        
        for (int i = 0; i < jsonData.jsonGameCardsInHandData.Count; i++)
        {
            m_hand.Add(GameCardFactory.GetCardFromJson(jsonData.jsonGameCardsInHandData[i]));
        }

        for (int i = 0; i < jsonData.jsonGameCardsInDiscardData.Count; i++)
        {
            m_cardsToDiscard.Add(GameCardFactory.GetCardFromJson(jsonData.jsonGameCardsInDiscardData[i]));
        }

        for (int i = 0; i < jsonData.jsonGameCardsInExileData.Count; i++)
        {
            if (jsonData.jsonGameCardsInExileData[i].jsonGameUnitXPosition.HasValue && jsonData.jsonGameCardsInExileData[i].jsonGameUnitYPosition.HasValue)
            {
                WorldTile worldTile = WorldGridManager.Instance.GetWorldGridTileAtPosition
                    (jsonData.jsonGameCardsInExileData[i].jsonGameUnitXPosition.Value, jsonData.jsonGameCardsInExileData[i].jsonGameUnitYPosition.Value);

                if (worldTile == null)
                {
                    return;
                }

                if (worldTile.GetGameTile().IsOccupied() && worldTile.GetGameTile().GetOccupyingUnit().GetGuid() == jsonData.jsonGameCardsInExileData[i].jsonGameUnitData.guid)
                {
                    m_cardsInExile.Add(GameCardFactory.GetCardFromUnit(worldTile.GetGameTile().GetOccupyingUnit()));
                    continue;
                }
            }
            m_cardsInExile.Add(GameCardFactory.GetCardFromJson(jsonData.jsonGameCardsInExileData[i]));
        }

        for (int i = 0; i < jsonData.jsonGameScheduledActionData.Count; i++)
        {
            ScheduledActionTime scheduledActionTime = (ScheduledActionTime)jsonData.jsonGameScheduledActionData[i].scheduledActionTime;

            if (jsonData.jsonGameScheduledActionData[i].jsonTargetUnitData == null)
            {
                GameAction gameAction = GameActionFactory.GetActionFromJson(jsonData.jsonGameScheduledActionData[i].jsonActionData);
                AddScheduledAction(scheduledActionTime, gameAction);
            }
            else
            {
                //Find gameUnit
                bool hasFoundGameUnit = false;
                
                List<GameCard> deckBaseMainDeck = m_deckBase.GetDeck();
                for (int k = 0; k < deckBaseMainDeck.Count; k++)
                {
                    if (deckBaseMainDeck[k] is GameUnitCard gameUnitCard && gameUnitCard.m_unit.GetGuid() == jsonData.jsonGameScheduledActionData[i].jsonTargetUnitData.guid)
                    {
                        GameAction gameAction = GameActionFactory.GetActionFromJson(jsonData.jsonGameScheduledActionData[i].jsonActionData, gameUnitCard.m_unit);
                        AddScheduledAction(scheduledActionTime, gameAction);
                        hasFoundGameUnit = true;
                        break;
                    }
                }

                if (hasFoundGameUnit)
                {
                    continue;
                }

                List<GameCard> deckBaseDiscard = m_deckBase.GetDiscard();
                for (int k = 0; k < deckBaseDiscard.Count; k++)
                {
                    if (deckBaseDiscard[k] is GameUnitCard gameUnitCard && gameUnitCard.m_unit.GetGuid() == jsonData.jsonGameScheduledActionData[i].jsonTargetUnitData.guid)
                    {
                        GameAction gameAction = GameActionFactory.GetActionFromJson(jsonData.jsonGameScheduledActionData[i].jsonActionData, gameUnitCard.m_unit);
                        AddScheduledAction(scheduledActionTime, gameAction);
                        hasFoundGameUnit = true;
                        break;
                    }
                }

                if (hasFoundGameUnit)
                {
                    continue;
                }

                for (int k = 0; k < m_cardsInExile.Count; k++)
                {
                    if (m_cardsInExile[k] is GameUnitCard gameUnitCard && gameUnitCard.m_unit.GetGuid() == jsonData.jsonGameScheduledActionData[i].jsonTargetUnitData.guid)
                    {
                        GameAction gameAction = GameActionFactory.GetActionFromJson(jsonData.jsonGameScheduledActionData[i].jsonActionData, gameUnitCard.m_unit);
                        AddScheduledAction(scheduledActionTime, gameAction);
                        hasFoundGameUnit = true;
                        break;
                    }
                }

                if (!hasFoundGameUnit)
                {
                    Debug.LogError("Failed to find unit for scheduled event.");
                }
            }
        }

        m_relics.LoadFromJson(jsonData.jsonGameRelicHolderData);

        m_spellsPlayedPreviousTurn = jsonData.spellsPlayedPreviousTurn;
        m_spellsPlayedThisTurn = jsonData.spellsPlayedThisTurn;
        m_fletchingPowerIncrease = jsonData.fletchingPowerIncrease;
        m_tempMagicPowerIncrease = jsonData.tempMagicPowerIncrease;
        m_totemOfTheWolfTurn = jsonData.totemOfTheWolfTurn;
        m_wallet.m_gold = jsonData.goldAmount;
    }
}
