using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameCardFactory
{
    public static GameCard GetRandomNonEventCard()
    {
        int r = Random.Range(0, 15);

        switch (r)
        {
            case 0:
                return new ContentCureWoundsCard();
            case 1:
                return new ContentDevourerCard();
            case 2:
                return new ContentDrainCard();
            case 3:
                return new ContentDwarvenSoldierCard();
            case 4:
                return new ContentElvenRogueCard();
            case 5:
                return new ContentElvenSentinelCard();
            case 6:
                return new ContentElvenWizardCard();
            case 7:
                return new ContentEnergizeCard();
            case 8:
                return new ContentFireboltCard();
            case 9:
                return new ContentFishOracleCard();
            case 10:
                return new ContentGoblinCard();
            case 11:
                return new ContentGroundskeeperCard();
            case 12:
                return new ContentInjuredTrollCard();
            case 13:
                return new ContentNaturalScoutCard();
            case 14:
                return new ContentSabobotCard();
            default:
                return null;
        }
    }
}

