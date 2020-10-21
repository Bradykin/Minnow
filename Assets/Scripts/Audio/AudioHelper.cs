using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioHelper
{
    public static AudioClip GetBackgroundMusic(string mapName)
    {
        return Resources.Load<AudioClip>("Audio/MapBackground/" + mapName) as AudioClip;
    }
}
