using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIReputationBarController : MonoBehaviour
{
    public Text m_reputationLevelText;
    public Text m_reputationValueText;

    void Update()
    {
        int reputationLevel = PlayerDataManager.GetCurLevel();
        int curRepVal = PlayerDataManager.GetProgressToNextLevel().Item1;
        int nextRepVal = PlayerDataManager.GetProgressToNextLevel().Item2;

        m_reputationLevelText.text = "Reputation Level: " + reputationLevel;
        m_reputationValueText.text = curRepVal + "/" + nextRepVal;
    }
}
