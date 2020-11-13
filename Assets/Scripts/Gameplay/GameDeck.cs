using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDeck : ILoad<JsonGameDeckData>, ISave<JsonGameDeckData>
{
    private List<GameCard> m_cards = new List<GameCard>();
    private List<GameCard> m_discard = new List<GameCard>();

    public GameDeck()
    {
    }

    public void FillStartingDeck()
    {
        for (int i = 0; i < 2; i++)
        {
            m_cards.Add(GameCardFactory.GetCardClone(GameCardFactory.GetCardByName(PlayerDataManager.PlayerAccountData.StarterSimpleUnitName)));
        }

        for (int i = 0; i < 1; i++)
        {
            m_cards.Add(GameCardFactory.GetCardClone(GameCardFactory.GetCardByName(PlayerDataManager.PlayerAccountData.StarterAdvancedUnitName)));
        }

        for (int i = 0; i < 3; i++)
        {
            m_cards.Add(GameCardFactory.GetCardClone(GameCardFactory.GetCardByName(PlayerDataManager.PlayerAccountData.StarterDamageSpellName)));
        }

        for (int i = 0; i < 2; i++)
        {
            m_cards.Add(GameCardFactory.GetCardClone(GameCardFactory.GetCardByName(PlayerDataManager.PlayerAccountData.StarterDefensiveSpellName)));
        }

        for (int i = 0; i < 1; i++)
        {
            m_cards.Add(GameCardFactory.GetCardClone(GameCardFactory.GetCardByName(PlayerDataManager.PlayerAccountData.StarterExileSpellName)));
        }

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddCards))
        {
            for (int i = 0; i < 2; i++)
            {
                m_cards.Add(GameCardFactory.GetRandomStandardSpellCard(GameElementBase.GameRarity.Common, GameCardFactory.m_tribalCards));
            }

            m_cards.Add(GameCardFactory.GetRandomStandardSpellCard(GameElementBase.GameRarity.Uncommon, GameCardFactory.m_tribalCards));
            m_cards.Add(GameCardFactory.GetRandomStandardUnitCard(GameElementBase.GameRarity.Uncommon, GameCardFactory.m_tribalCards));
        }
    }

    public List<GameCard> GetDeck()
    {
        return m_cards;
    }

    public List<GameCard> GetDiscard()
    {
        return m_discard;
    }

    public int Count()
    {
        return m_cards.Count;
    }

    public int DiscardCount()
    {
        return m_discard.Count;
    }

    public void ClearDeck()
    {
        m_cards = new List<GameCard>();
    }

    public GameCard GetCardByIndex(int index)
    {
        if (index >= m_cards.Count)
        {
            return null;
        }

        return m_cards[index];
    }

    public void RemoveCard(GameCard toRemove)
    {
        m_cards.Remove(toRemove);
        m_discard.Remove(toRemove);
    }

    public void AddCard(GameCard card)
    {
        m_cards.Add(card);
    }

    public void AddToDiscard(GameCard card)
    {
        m_discard.Add(card);
    }

    public void Shuffle()
    {
        for (int i = 0; i < m_cards.Count; i++)
        {
            GameCard temp = m_cards[i];
            int randomIndex = Random.Range(i, m_cards.Count);
            m_cards[i] = m_cards[randomIndex];
            m_cards[randomIndex] = temp;
        }

        //After shuffle, put an unit on top of library
        for (int i = 0; i < m_cards.Count; i++)
        {
            if (m_cards[i] is GameUnitCard)
            {
                GameCard tempCard = m_cards[0];
                m_cards[0] = m_cards[i];
                m_cards[i] = tempCard;
                break;
            }
        }
    }

    public GameCard DrawCard()
    {
        if (m_cards.Count == 0)
        {
            ShuffleDiscard();
        }

        if (m_cards.Count == 0) //This means that both the deck and the discard are empty
        {
            UIHelper.CreateDeckWorldElementNotification("No cards left.");
            return null;
        }

        GameCard toReturn = m_cards[0];
        m_cards.RemoveAt(0);
        return toReturn;
    }

    private void ShuffleDiscard()
    {
        m_cards.AddRange(m_discard);
        m_discard = new List<GameCard>();

        Shuffle();
    }

    public List<GameCard> GetCardsForRead()
    {
        return m_cards;
    }

    //============================================================================================================//

    public JsonGameDeckData SaveToJson()
    {
        JsonGameDeckData jsonData = new JsonGameDeckData
        {
            jsonGameCardsInDeckData = new List<JsonGameCardData>(),
            jsonGameCardsInDiscardData = new List<JsonGameCardData>()
        };

        for (int i = 0; i < m_cards.Count; i++)
        {
            jsonData.jsonGameCardsInDeckData.Add(m_cards[i].SaveToJson());
        }

        for (int i = 0; i < m_discard.Count; i++)
        {
            jsonData.jsonGameCardsInDiscardData.Add(m_discard[i].SaveToJson());
        }

        return jsonData;
    }

    public void LoadFromJson(JsonGameDeckData jsonData)
    {
        for (int i = 0; i < jsonData.jsonGameCardsInDeckData.Count; i++)
        {
            if (jsonData.jsonGameCardsInDeckData[i].jsonGameUnitXPosition.HasValue && jsonData.jsonGameCardsInDeckData[i].jsonGameUnitYPosition.HasValue)
            {
                WorldTile worldTile = WorldGridManager.Instance.GetWorldGridTileAtPosition
                    (jsonData.jsonGameCardsInDeckData[i].jsonGameUnitXPosition.Value, jsonData.jsonGameCardsInDeckData[i].jsonGameUnitYPosition.Value);

                if (worldTile == null)
                {
                    return;
                }

                if (worldTile.GetGameTile().IsOccupied() && worldTile.GetGameTile().m_occupyingUnit.GetBaseName() == jsonData.jsonGameCardsInDeckData[i].baseName)
                {
                    m_cards.Add(GameCardFactory.GetCardFromUnit(worldTile.GetGameTile().m_occupyingUnit));
                    continue;
                }
            }
            m_cards.Add(GameCardFactory.GetCardFromJson(jsonData.jsonGameCardsInDeckData[i]));
        }

        for (int i = 0; i < jsonData.jsonGameCardsInDiscardData.Count; i++)
        {
            if (jsonData.jsonGameCardsInDiscardData[i].jsonGameUnitXPosition.HasValue && jsonData.jsonGameCardsInDiscardData[i].jsonGameUnitYPosition.HasValue)
            {
                WorldTile worldTile = WorldGridManager.Instance.GetWorldGridTileAtPosition
                    (jsonData.jsonGameCardsInDiscardData[i].jsonGameUnitXPosition.Value, jsonData.jsonGameCardsInDiscardData[i].jsonGameUnitYPosition.Value);

                if (worldTile == null)
                {
                    return;
                }

                if (worldTile.GetGameTile().IsOccupied() && worldTile.GetGameTile().m_occupyingUnit.GetBaseName() == jsonData.jsonGameCardsInDiscardData[i].baseName)
                {
                    m_discard.Add(GameCardFactory.GetCardFromUnit(worldTile.GetGameTile().m_occupyingUnit));
                    continue;
                }
            }
            m_discard.Add(GameCardFactory.GetCardFromJson(jsonData.jsonGameCardsInDiscardData[i]));
        }
    }
}
