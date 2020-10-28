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
}