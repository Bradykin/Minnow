using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameRelicFactory
{
    public static GameRelic GetRandomRelic()
    {
        int r = Random.Range(0, 11);

        switch (r)
        {
            case 0:
                return new ContentDominerickRefrainRelic();
            case 1:
                return new ContentHourglassOfSpeedRelic();
            case 2:
                return new ContentMaskOfAgesRelic();
            case 3:
                return new ContentMorlemainsSkullRelic();
            case 4:
                return new ContentMysticRuneRelic();
            case 5:
                return new ContentOrbOfEnergyRelic();
            case 6:
                return new ContentOrbOfHealthRelic();
            case 7:
                return new ContentSecretSoupRelic();
            case 8:
                return new ContentSoulTrapRelic();
            case 9:
                return new ContentSpiritCatcherRelic();
            case 10:
                return new ContentWolvenFangRelic();
            default:
                return null;
        }
    }
}

