using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioHelper
{
    public static float DefaultMusicVolume = 0.6f;
    public static float DefaultSFXVolume = 0.7f;

    //UI SFX
    public static AudioClip UIHover = Resources.Load<AudioClip>("Audio/SFX/UI/UIHover") as AudioClip;
    public static AudioClip UIClick = Resources.Load<AudioClip>("Audio/SFX/UI/UIClick") as AudioClip;
    public static AudioClip UICardHover = Resources.Load<AudioClip>("Audio/SFX/UI/UICardHover") as AudioClip;
    public static AudioClip UICardClick = Resources.Load<AudioClip>("Audio/SFX/UI/UICardClick") as AudioClip;
    public static AudioClip UIHudNotification = Resources.Load<AudioClip>("Audio/SFX/UI/UIHudNotification") as AudioClip;
    public static AudioClip UIError = Resources.Load<AudioClip>("Audio/SFX/UI/UIError") as AudioClip;
    public static AudioClip WorldUnitClick = Resources.Load<AudioClip>("Audio/SFX/UI/WorldUnitClick") as AudioClip;

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
    public static AudioClip BirdFlap = Resources.Load<AudioClip>("Audio/SFX/UnitAttack/BirdFlap") as AudioClip;

    //Spell Play SFX
    public static AudioClip LargeImpact = Resources.Load<AudioClip>("Audio/SFX/Spells/LargeImpact") as AudioClip;
    public static AudioClip FireBlast = Resources.Load<AudioClip>("Audio/SFX/Spells/FireBlast") as AudioClip;
    public static AudioClip SmallBuff = Resources.Load<AudioClip>("Audio/SFX/Spells/SmallBuff") as AudioClip;
    public static AudioClip SmallDebuff = Resources.Load<AudioClip>("Audio/SFX/Spells/SmallDebuff") as AudioClip;
    public static AudioClip MiscEffect = Resources.Load<AudioClip>("Audio/SFX/Spells/MiscEffect") as AudioClip;
    public static AudioClip MagicBolt = Resources.Load<AudioClip>("Audio/SFX/Spells/MagicBolt") as AudioClip;
    public static AudioClip DaggerSwingSpell = Resources.Load<AudioClip>("Audio/SFX/Spells/DaggerSwingSpell") as AudioClip;
    public static AudioClip BowSpell = Resources.Load<AudioClip>("Audio/SFX/Spells/BowSpell") as AudioClip;
    public static AudioClip SciFiBuffSmall = Resources.Load<AudioClip>("Audio/SFX/Spells/SciFiBuffSmall") as AudioClip;
    public static AudioClip BloodSacrifice = Resources.Load<AudioClip>("Audio/SFX/Spells/BloodSacrifice") as AudioClip;
    public static AudioClip Bullheaded = Resources.Load<AudioClip>("Audio/SFX/Spells/Bullheaded") as AudioClip;
    public static AudioClip Heal = Resources.Load<AudioClip>("Audio/SFX/Spells/Heal") as AudioClip;
    public static AudioClip Energize = Resources.Load<AudioClip>("Audio/SFX/Spells/Energize") as AudioClip;
    public static AudioClip Fury = Resources.Load<AudioClip>("Audio/SFX/Spells/Fury") as AudioClip;
    public static AudioClip GoldSpell = Resources.Load<AudioClip>("Audio/SFX/Spells/GoldSpell") as AudioClip;
    public static AudioClip MetalBuff = Resources.Load<AudioClip>("Audio/SFX/Spells/MetalBuff") as AudioClip;
    public static AudioClip NecromanticTouch = Resources.Load<AudioClip>("Audio/SFX/Spells/NecromanticTouch") as AudioClip;
    public static AudioClip MagicEffect = Resources.Load<AudioClip>("Audio/SFX/Spells/MagicEffect") as AudioClip;
    public static AudioClip LightningBolt = Resources.Load<AudioClip>("Audio/SFX/Spells/LightningBolt") as AudioClip;
    public static AudioClip TreeGrow = Resources.Load<AudioClip>("Audio/SFX/Spells/TreeGrow") as AudioClip;

    //World Perk Pickup
    public static AudioClip GoldPickupSmall = Resources.Load<AudioClip>("Audio/SFX/WorldPerk/GoldPickupSmall") as AudioClip;
    public static AudioClip GoldPickupMedium = Resources.Load<AudioClip>("Audio/SFX/WorldPerk/GoldPickupMedium") as AudioClip;
    public static AudioClip GoldPickupLarge = Resources.Load<AudioClip>("Audio/SFX/WorldPerk/GoldPickupLarge") as AudioClip;
    public static AudioClip GoldSpend = Resources.Load<AudioClip>("Audio/SFX/WorldPerk/GoldSpend") as AudioClip;

    //Special Actions
    public static AudioClip ShivcasterThrowShivs = Resources.Load<AudioClip>("Audio/SFX/SpecialActions/ShivcasterThrowShivs") as AudioClip;
    public static AudioClip GainRandomSpellCard = Resources.Load<AudioClip>("Audio/SFX/SpecialActions/GainRandomSpellCard") as AudioClip;

    //Movement SFX
    public static AudioClip BuildingMovement = Resources.Load<AudioClip>("Audio/SFX/TerrainMovement/BuildingMovement") as AudioClip;
    public static AudioClip DesertMovement = Resources.Load<AudioClip>("Audio/SFX/TerrainMovement/DesertMovement") as AudioClip;
    public static AudioClip ForestMovement = Resources.Load<AudioClip>("Audio/SFX/TerrainMovement/ForestMovement") as AudioClip;
    public static AudioClip HillMovement = Resources.Load<AudioClip>("Audio/SFX/TerrainMovement/HillMovement") as AudioClip;
    public static AudioClip MountainMovement = Resources.Load<AudioClip>("Audio/SFX/TerrainMovement/MountainMovement") as AudioClip;
    public static AudioClip PlainsMovement = Resources.Load<AudioClip>("Audio/SFX/TerrainMovement/PlainsMovement") as AudioClip;
    public static AudioClip SnowMovement = Resources.Load<AudioClip>("Audio/SFX/TerrainMovement/SnowMovement") as AudioClip;
    public static AudioClip WaterMovement = Resources.Load<AudioClip>("Audio/SFX/TerrainMovement/WaterMovement") as AudioClip;

    //Background Music
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

    public static void PlaySFXForWalkOnTile(GameTile tile, GameUnit walkUnit)
    {
        if (walkUnit.GetFlyingKeyword() != null)
        {
            return;
        }

        if (walkUnit.GetTeam() == Team.Enemy)
        {
            if (!PlayerDataManager.PlayerAccountData.m_followEnemy)
            {
                return;
            }

            if (tile.m_isFog)
            {
                return;
            }
        }

        if (tile.HasBuilding())
        {
            PlaySFX(BuildingMovement);
        }
        else if (tile.GetTerrain().IsWater())
        {
            PlaySFX(WaterMovement);
        }
        else if (tile.GetTerrain().IsDunes())
        {
            PlaySFX(DesertMovement);
        }
        else if (tile.GetTerrain().IsForest())
        {
            PlaySFX(ForestMovement);
        }
        else if (tile.GetTerrain().IsHill())
        {
            PlaySFX(HillMovement);
        }
        else if (tile.GetTerrain().IsMountain())
        {
            PlaySFX(MountainMovement);
        }
        else if (tile.GetTerrain().IsPlains())
        {
            PlaySFX(PlainsMovement);
        }
        else if (tile.GetTerrain().IsSnow())
        {
            PlaySFX(SnowMovement);
        }
    }
}
