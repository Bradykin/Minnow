using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentAncientEvilRelic : GameRelic
{
    private int m_killCount = 0;
    private bool m_isTransformed = false;

    public ContentAncientEvilRelic()
    {
        m_name = "Ancient Evil";
        m_desc = "When you pick this up, gain 1 gold.";
        //If 50 enemies are killed after getting this, all allied units are monsters and allied monsters get +10/+10.
        m_rarity = GameRarity.Rare;

        LateInit();
    }

    public void AddKillCount()
    {
        m_killCount++;

        if (m_killCount == 50)
        {
            m_isTransformed = true;

            UIHelper.CreateHUDNotification("Evil Awakens", "The Ancient Evil Awakens.");
            m_desc = "All allied units are <b>Monster</b> units and allied <b>Monster</b> units get +10/+10";
        }
        else if (m_killCount < 50 && m_killCount%10 == 0)
        {
            UIHelper.CreateHUDNotification("Evil Stirs", "As the blood of the enemies fall, an Ancient Evil stirs.");
        }
    }

    public bool IsTransformed()
    {
        return m_isTransformed;
    }

    public override JsonGameRelicData SaveToJson()
    {
        JsonGameRelicData jsonData = base.SaveToJson();

        jsonData.intValue = m_killCount;
        jsonData.boolValue = m_isTransformed;

        return jsonData;
    }

    public override void LoadFromJson(JsonGameRelicData jsonData)
    {
        base.LoadFromJson(jsonData);

        m_killCount = jsonData.intValue;
        m_isTransformed = jsonData.boolValue;
    }
}