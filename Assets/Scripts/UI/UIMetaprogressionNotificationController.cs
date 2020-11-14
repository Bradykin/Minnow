using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIMetaprogressionNotificationController
{
    private static List<GameMetaprogressionReward> m_rewardsToShow = new List<GameMetaprogressionReward>();

    public static bool HasMultipleNotifications()
    {
        return m_rewardsToShow.Count > 1;
    }

    public static bool HasAnyNotifications()
    {
        return m_rewardsToShow.Count > 0;
    }

    public static void AcceptReward()
    {
        m_rewardsToShow.RemoveAt(0);
    }

    public static void AddReward(GameMetaprogressionReward reward)
    {
        m_rewardsToShow.Add(reward);
    }

    public static GameMetaprogressionReward GetReward()
    {
        return m_rewardsToShow[0];
    }
}
