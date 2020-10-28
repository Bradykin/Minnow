using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioHelper
{
    public static AudioClip UIHover = Resources.Load<AudioClip>("Audio/SFX/UIHover") as AudioClip;
    public static AudioClip UIClick = Resources.Load<AudioClip>("Audio/SFX/UIClick") as AudioClip;

    public static AudioClip WorldUnitHover = Resources.Load<AudioClip>("Audio/SFX/WorldUnitHover") as AudioClip;
    public static AudioClip WorldUnitClick = Resources.Load<AudioClip>("Audio/SFX/WorldUnitClick") as AudioClip;

    public static AudioClip UICardHover = Resources.Load<AudioClip>("Audio/SFX/UICardHover") as AudioClip;
    public static AudioClip UICardClick = Resources.Load<AudioClip>("Audio/SFX/UICardClick") as AudioClip;

    public static AudioClip UIError = Resources.Load<AudioClip>("Audio/SFX/UIError") as AudioClip;

    public static AudioClip GetBackgroundMusic(string mapName)
    {
        return Resources.Load<AudioClip>("Audio/MapBackground/" + mapName) as AudioClip;
    }

    public static void PlaySFX(AudioClip toPlay)
    {
        AudioSFXController.Instance.PlaySFX(toPlay);
    }
}
