using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioHelper
{
    public static AudioClip UIClick = Resources.Load<AudioClip>("Audio/SFX/UIClick") as AudioClip;
    public static AudioClip UIHover = Resources.Load<AudioClip>("Audio/SFX/UIHover") as AudioClip;

    public static AudioClip GetBackgroundMusic(string mapName)
    {
        return Resources.Load<AudioClip>("Audio/MapBackground/" + mapName) as AudioClip;
    }

    public static void PlaySFX(AudioClip toPlay)
    {
        AudioSFXController.Instance.PlaySFX(toPlay);
    }
}
