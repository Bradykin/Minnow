﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWorldMap : MonoBehaviour
{
    public List<UILevelSelectButton> m_levelSelectButtons = new List<UILevelSelectButton>();

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < m_levelSelectButtons.Count; i++)
        {
            if (m_levelSelectButtons[i].GetMap() == null)
            {
                continue;
            }

            m_levelSelectButtons[i].gameObject.SetActive((!m_levelSelectButtons[i].GetMap().m_disableUnfinished) && (Constants.UnlockAllContent || GameMetaprogressionUnlocksDataManager.HasUnlocked(m_levelSelectButtons[i].GetMap())));
        }
    }
}
