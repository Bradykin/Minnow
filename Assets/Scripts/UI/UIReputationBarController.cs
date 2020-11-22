using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIReputationBarController : MonoBehaviour
{
    public TMP_Text m_reputationLevelText;
    public TMP_Text m_reputationValueText;

    public GameObject m_holder;

    void Update()
    {
        int reputationLevel = PlayerDataManager.GetCurLevel();
        int curRepVal = PlayerDataManager.GetProgressToNextLevel().Item1;
        int nextRepVal = PlayerDataManager.GetProgressToNextLevel().Item2;

        if (reputationLevel < 2 && !Constants.UnlockAllContent)
        {
            m_holder.SetActive(false);
            return;
        }

        m_holder.SetActive(true);

        m_reputationLevelText.text = "Reputation Level: " + reputationLevel;
        m_reputationValueText.text = curRepVal + "/" + nextRepVal;
    }
}
