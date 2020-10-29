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

    public static AudioClip UIHover = Resources.Load<AudioClip>("Audio/SFX/UIHover") as AudioClip;
    public static AudioClip UIClick = Resources.Load<AudioClip>("Audio/SFX/UIClick") as AudioClip;

    public static AudioClip WorldUnitHover = Resources.Load<AudioClip>("Audio/SFX/WorldUnitHover") as AudioClip;
    public static AudioClip WorldUnitClick = Resources.Load<AudioClip>("Audio/SFX/WorldUnitClick") as AudioClip;

    public static AudioClip UICardHover = Resources.Load<AudioClip>("Audio/SFX/UICardHover") as AudioClip;
    public static AudioClip UICardClick = Resources.Load<AudioClip>("Audio/SFX/UICardClick") as AudioClip;

    public static AudioClip UIError = Resources.Load<AudioClip>("Audio/SFX/UIError") as AudioClip;

    public static AudioClip BuffSpellCategory = Resources.Load<AudioClip>("Audio/SFX/BuffSpellCategory") as AudioClip;
    public static AudioClip DebuffSpellCategory = Resources.Load<AudioClip>("Audio/SFX/DebuffSpellCategory") as AudioClip;
    public static AudioClip DamageSpellCategory = Resources.Load<AudioClip>("Audio/SFX/DamageSpellCategory") as AudioClip;

    public static AudioClip UnitGetHit = Resources.Load<AudioClip>("Audio/SFX/UnitGetHit") as AudioClip;

    public static AudioClip GetBackgroundMusic(string mapName)
    {
        return Resources.Load<AudioClip>("Audio/MapBackground/" + mapName) as AudioClip;
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
