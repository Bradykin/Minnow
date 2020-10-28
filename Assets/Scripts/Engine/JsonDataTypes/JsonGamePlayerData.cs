using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct JsonGamePlayerData
{
    public JsonGameDeckData jsonDeckBaseData;
    public JsonGameDeckData jsonDeckCurrentData;

    public List<JsonGameCardData> jsonCardsInHandData;
    public List<JsonGameCardData> jsonCardsInDiscardData;
    public List<JsonGameCardData> jsonCardsInExileData;
}