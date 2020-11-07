using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct JsonGamePlayerData
{
    public int maxEnergy;
    public int curEnergy;
    public int maxActions;
    public int curActions;
    
    public JsonGameDeckData jsonDeckBaseData;
    public JsonGameDeckData jsonDeckCurrentData;

    public List<JsonGameCardData> jsonGameCardsInHandData;
    public List<JsonGameCardData> jsonGameCardsInDiscardData;
    public List<JsonGameCardData> jsonGameCardsInExileData;

    public List<JsonGameScheduledActionData> jsonGameScheduledActionData;

    public int goldAmount;

    public int spellsPlayedPreviousTurn;
    public int spellsPlayedThisTurn;
    public int fletchingPowerIncrease;
    public int tempMagicPowerIncrease;
    public int totemOfTheWolfTurn;
}