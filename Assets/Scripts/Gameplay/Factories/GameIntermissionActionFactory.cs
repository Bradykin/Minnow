using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameIntermissionActionFactory
{
    private static List<GameActionIntermission> m_intermissionActions = new List<GameActionIntermission>();

    private static bool m_hasInit = false;

    public static void Init()
    {
        m_hasInit = true;

        m_intermissionActions.Add(new ContentResourcesIntermissionAction());
        m_intermissionActions.Add(new ContentBuffUnitIntermissionAction());
        m_intermissionActions.Add(new ContentCardIntermissionAction());
        m_intermissionActions.Add(new ContentRemovalIntermissionAction());
        m_intermissionActions.Add(new ContentGainMagicPowerIntermissionAction());
        m_intermissionActions.Add(new ContentRebuildIntermissionAction());
        m_intermissionActions.Add(new ContentTransformUnitIntermissionAction());
        m_intermissionActions.Add(new ContentDuplicateSpellIntermissionAction());
        m_intermissionActions.Add(new ContentRelicIntermissionAction());
    }

    public static GameActionIntermission GetRandomAction(List<GameActionIntermission> exclusionList = null)
    {
        if (!m_hasInit)
        {
            Init();
        }

        List<GameActionIntermission> actions = new List<GameActionIntermission>();
        for (int i = 0; i < m_intermissionActions.Count; i++)
        {
            bool hasInExclusion = false;
            if (exclusionList == null)
            {
                if (m_intermissionActions[i].IsValidToSpawn())
                {
                    actions.Add(m_intermissionActions[i]);
                }
            }
            else
            {
                for (int c = 0; c < exclusionList.Count; c++)
                {
                    if (exclusionList[c].GetName() == m_intermissionActions[i].GetName())
                    {
                        hasInExclusion = true;
                        break;
                    }
                }

                if (!hasInExclusion)
                {
                    if (m_intermissionActions[i].IsValidToSpawn())
                    {
                        actions.Add(m_intermissionActions[i]);
                    }
                }
            }
        }

        int r = UnityEngine.Random.Range(0, actions.Count);
        return (GameActionIntermission)Activator.CreateInstance(actions[r].GetType());
    }
}
