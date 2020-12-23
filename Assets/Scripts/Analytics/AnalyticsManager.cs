using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsManager
{
    public void RecordCardChoice(in GameCard cardChoice, in GameCard optionOne, in GameCard optionTwo, in GameCard optionThree)
    {
        
    }

    public void RecordCardSingleChoice(in GameCard cardOption, bool taken)
    {

    }

    public void RecordCardDuplication(in GameCard cardDuplicated)
    {

    }

    public void RecordCardTransformation(in GameCard cardTransformed, in GameCard cardReceived)
    {

    }

    public void RecordCardRemoval(in GameCard cardRemoved)
    {

    }

    public void RecordRelicChoice(in GameRelic relicChoice, in GameRelic optionOne, in GameRelic optionTwo)
    {
        
    }

    public void RecordRelicSingleChoice(in GameRelic relicOption, bool taken)
    {

    }
}
