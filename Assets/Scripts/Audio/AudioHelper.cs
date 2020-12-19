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

    //Unit Attack SFX
    public static AudioClip LazerAttack = Resources.Load<AudioClip>("Audio/SFX/UnitAttack/LazerAttack") as AudioClip;
    public static AudioClip SpellAttackMedium = Resources.Load<AudioClip>("Audio/SFX/UnitAttack/SpellAttackMedium") as AudioClip;
    public static AudioClip SwordHeavy = Resources.Load<AudioClip>("Audio/SFX/UnitAttack/SwordHeavy") as AudioClip;
    public static AudioClip SwordLight = Resources.Load<AudioClip>("Audio/SFX/UnitAttack/SwordLight") as AudioClip;
    public static AudioClip DaggerLight = Resources.Load<AudioClip>("Audio/SFX/UnitAttack/DaggerLight") as AudioClip;
    public static AudioClip DaggerThrown = Resources.Load<AudioClip>("Audio/SFX/UnitAttack/DaggerThrown") as AudioClip;
    public static AudioClip SpearHeavy = Resources.Load<AudioClip>("Audio/SFX/UnitAttack/SpearHeavy") as AudioClip;
    public static AudioClip SpearLight = Resources.Load<AudioClip>("Audio/SFX/UnitAttack/SpearLight") as AudioClip;
    public static AudioClip MaceMedium = Resources.Load<AudioClip>("Audio/SFX/UnitAttack/MaceMedium") as AudioClip;
    public static AudioClip BowLight = Resources.Load<AudioClip>("Audio/SFX/UnitAttack/BowLight") as AudioClip;
    public static AudioClip BowHeavy = Resources.Load<AudioClip>("Audio/SFX/UnitAttack/BowHeavy") as AudioClip;
    public static AudioClip SlurpAttack = Resources.Load<AudioClip>("Audio/SFX/UnitAttack/SlurpAttack") as AudioClip;
    public static AudioClip PunchLight = Resources.Load<AudioClip>("Audio/SFX/UnitAttack/PunchLight") as AudioClip;
    public static AudioClip SlamHeavy = Resources.Load<AudioClip>("Audio/SFX/UnitAttack/SlamHeavy") as AudioClip;
    public static AudioClip Roar = Resources.Load<AudioClip>("Audio/SFX/UnitAttack/Roar") as AudioClip;
    public static AudioClip RaptorAttack = Resources.Load<AudioClip>("Audio/SFX/UnitAttack/RaptorAttack") as AudioClip;
    public static AudioClip BoarAttack = Resources.Load<AudioClip>("Audio/SFX/UnitAttack/BoarAttack") as AudioClip;
    public static AudioClip MetalClangAttack = Resources.Load<AudioClip>("Audio/SFX/UnitAttack/MetalClangAttack") as AudioClip;

    //Spell Play SFX
    public static AudioClip LargeImpact = Resources.Load<AudioClip>("Audio/SFX/Spells/LargeImpact") as AudioClip;
    public static AudioClip FireBlast = Resources.Load<AudioClip>("Audio/SFX/Spells/FireBlast") as AudioClip;
    public static AudioClip SmallBuff = Resources.Load<AudioClip>("Audio/SFX/Spells/SmallBuff") as AudioClip;
    public static AudioClip SmallDebuff = Resources.Load<AudioClip>("Audio/SFX/Spells/SmallDebuff") as AudioClip;

    //World Perk Pickup
    public static AudioClip GoldPickupSmall = Resources.Load<AudioClip>("Audio/SFX/WorldPerk/GoldPickupSmall") as AudioClip;
    public static AudioClip GoldPickupMedium = Resources.Load<AudioClip>("Audio/SFX/WorldPerk/GoldPickupMedium") as AudioClip;
    public static AudioClip GoldPickupLarge = Resources.Load<AudioClip>("Audio/SFX/WorldPerk/GoldPickupLarge") as AudioClip;
    public static AudioClip GoldSpend = Resources.Load<AudioClip>("Audio/SFX/WorldPerk/GoldSpend") as AudioClip;

    //Special Actions
    public static AudioClip ShivcasterThrowShivs = Resources.Load<AudioClip>("Audio/SFX/SpecialActions/ShivcasterThrowShivs") as AudioClip;
    public static AudioClip GainRandomSpellCard = Resources.Load<AudioClip>("Audio/SFX/SpecialActions/GainRandomSpellCard") as AudioClip;

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
}
