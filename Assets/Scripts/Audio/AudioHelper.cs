using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioHelper
{
    public enum SpellAudioCategory
    {
        Buff,
        Debuff,
        Damage
    }

    public static float DefaultMusicVolume = 0.6f;
    public static float DefaultSFXVolume = 0.4f;

    public static AudioClip UIHover = Resources.Load<AudioClip>("Audio/SFX/UIHover") as AudioClip;
    public static AudioClip UIClick = Resources.Load<AudioClip>("Audio/SFX/UIClick") as AudioClip;

    public static AudioClip WorldUnitClick = Resources.Load<AudioClip>("Audio/SFX/WorldUnitClick") as AudioClip;

    public static AudioClip UICardHover = Resources.Load<AudioClip>("Audio/SFX/UICardHover") as AudioClip;
    public static AudioClip UICardClick = Resources.Load<AudioClip>("Audio/SFX/UICardClick") as AudioClip;
    public static AudioClip UIHudNotification = Resources.Load<AudioClip>("Audio/SFX/UIHudNotification") as AudioClip;

    public static AudioClip UIError = Resources.Load<AudioClip>("Audio/SFX/UIError") as AudioClip;

    public static AudioClip BuffSpellCategory = Resources.Load<AudioClip>("Audio/SFX/BuffSpellCategory") as AudioClip;
    public static AudioClip DebuffSpellCategory = Resources.Load<AudioClip>("Audio/SFX/DebuffSpellCategory") as AudioClip;
    public static AudioClip DamageSpellCategory = Resources.Load<AudioClip>("Audio/SFX/DamageSpellCategory") as AudioClip;

    public static AudioClip UnitGetHit = Resources.Load<AudioClip>("Audio/SFX/UnitGetHit") as AudioClip;

    public static AudioClip MenuBackgroundMusic = Resources.Load<AudioClip>("Audio/MapBackground/Main Menu") as AudioClip;

    private static Dictionary<string, AudioClip> m_backgroundMusicDictionary = new Dictionary<string, AudioClip>();

    public static AudioClip GetBackgroundMusic(string mapName)
    {
        if (m_backgroundMusicDictionary.ContainsKey(mapName))
        {
            return m_backgroundMusicDictionary[mapName];
        }
        
        AudioClip loadedClip = Resources.Load<AudioClip>("Audio/MapBackground/" + mapName) as AudioClip;
        m_backgroundMusicDictionary.Add(mapName, loadedClip);

        return loadedClip;
    }

    public static void PlaySFX(AudioClip toPlay)
    {
        AudioSFXController.Instance.PlaySFX(toPlay);
    }

    public static void PlaySFX(SpellAudioCategory spellCategory)
    {
        if (spellCategory == SpellAudioCategory.Buff)
        {
            AudioSFXController.Instance.PlaySFX(BuffSpellCategory);
        }
        else if (spellCategory == SpellAudioCategory.Debuff)
        {
            AudioSFXController.Instance.PlaySFX(DebuffSpellCategory);
        }
        else if (spellCategory == SpellAudioCategory.Damage)
        {
            AudioSFXController.Instance.PlaySFX(DamageSpellCategory);
        }
    }
}
