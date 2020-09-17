using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameNamesFactory
{
    private static List<string> m_humanoidNames = new List<string>();
    private static List<string> m_monsterNames = new List<string>();
    private static List<string> m_constructNames = new List<string>();


    private static bool m_hasInit = false;

    public static void Init()
    {
        m_humanoidNames.Add("Cranog");
        m_humanoidNames.Add("Hywel");
        m_humanoidNames.Add("Lewis");
        m_humanoidNames.Add("Coslett");
        m_humanoidNames.Add("Owin");
        m_humanoidNames.Add("Roberts");
        m_humanoidNames.Add("Guffin");
        m_humanoidNames.Add("Binner");
        m_humanoidNames.Add("Seth");
        m_humanoidNames.Add("Belling");
        m_humanoidNames.Add("Eryi");
        m_humanoidNames.Add("Trevor");
        m_humanoidNames.Add("Talisin");
        m_humanoidNames.Add("Derwyn");
        m_humanoidNames.Add("Moss");
        m_humanoidNames.Add("Beinon");
        m_humanoidNames.Add("Vaughan");
        m_humanoidNames.Add("Einian");
        m_humanoidNames.Add("Prichard");
        m_humanoidNames.Add("Llew");
        m_humanoidNames.Add("Dewey");
        m_humanoidNames.Add("Efrog");
        m_humanoidNames.Add("Glad");
        m_humanoidNames.Add("Gwawl");
        m_humanoidNames.Add("Watkins");
        m_humanoidNames.Add("Pennant");
        m_humanoidNames.Add("Howells");
        m_humanoidNames.Add("Evrawg");
        m_humanoidNames.Add("Daniels");
        m_humanoidNames.Add("Tudful");

        m_monsterNames.Add("Glowdeviation");
        m_monsterNames.Add("Fogstep");
        m_monsterNames.Add("Tomblich");
        m_monsterNames.Add("Hollowsoul");
        m_monsterNames.Add("Hellmask");
        m_monsterNames.Add("Trancefigure");
        m_monsterNames.Add("Hollowmorph");
        m_monsterNames.Add("Thornthing");
        m_monsterNames.Add("Dustbody");
        m_monsterNames.Add("Dreampaw");
        m_monsterNames.Add("Glowsnare");
        m_monsterNames.Add("Smokebrute");
        m_monsterNames.Add("Chaosfigure");
        m_monsterNames.Add("Vampling");
        m_monsterNames.Add("Blightman");
        m_monsterNames.Add("Toxinvine");
        m_monsterNames.Add("Venomling");
        m_monsterNames.Add("Flamestrike");
        m_monsterNames.Add("Spiritfang");
        m_monsterNames.Add("Soulpaw");
        m_monsterNames.Add("Vortexhag");
        m_monsterNames.Add("Auracat");
        m_monsterNames.Add("Netherghoul");
        m_monsterNames.Add("Mistsoul");
        m_monsterNames.Add("Chaosling");
        m_monsterNames.Add("Grievefigure");
        m_monsterNames.Add("Dreambrood");
        m_monsterNames.Add("Gasmonster");
        m_monsterNames.Add("Acidgolem");
        m_monsterNames.Add("Vilescream");

        m_constructNames.Add("Guc");
        m_constructNames.Add("Dolgumm");
        m_constructNames.Add("Ghurbor");
        m_constructNames.Add("Dhollehm");
        m_constructNames.Add("Gheldru");
        m_constructNames.Add("Geror");
        m_constructNames.Add("Lekiva");
        m_constructNames.Add("Nekhael");
        m_constructNames.Add("Yissalan");
        m_constructNames.Add("Elaraim");
        m_constructNames.Add("Vod");
        m_constructNames.Add("Buzguhm");
        m_constructNames.Add("Rhar");
        m_constructNames.Add("Bhom");
        m_constructNames.Add("Guklahn");
        m_constructNames.Add("Binyim");
        m_constructNames.Add("Nadon");
        m_constructNames.Add("Itan");
        m_constructNames.Add("Nazer");
        m_constructNames.Add("Hamog");
        m_constructNames.Add("Ghagzu");
        m_constructNames.Add("Zud");
        m_constructNames.Add("Ghon");
        m_constructNames.Add("Bellem");
        m_constructNames.Add("Brehn");
        m_constructNames.Add("Uraim");
        m_constructNames.Add("Yissaham");
        m_constructNames.Add("Evron");
        m_constructNames.Add("Umi");
        m_constructNames.Add("Aminiv");
    }

    public static string GetCustomEntityName(Typeline entityType)
    {
        if (!m_hasInit)
        {
            Init();
        }

        if (entityType == Typeline.Humanoid)
        {
            return GetNameFromList(m_humanoidNames);
        }
        else if (entityType == Typeline.Construct)
        {
            return GetNameFromList(m_constructNames);
        }
        else if (entityType == Typeline.Monster)
        {
            return GetNameFromList(m_monsterNames);
        }

        return "Error: No name?";
    }

    private static string GetNameFromList(List<string> toGetFrom)
    {
        string toReturn = toGetFrom[Random.Range(0, toGetFrom.Count)];

        return toReturn;
    }
}
