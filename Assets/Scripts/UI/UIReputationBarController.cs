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
        int reputationLevel = 6;
        int curRepVal = 1250;
        int nextRepVal = 2000;

        m_reputationLevelText.text = "Reputation Level: " + reputationLevel;
        m_reputationValueText.text = curRepVal + "/" + nextRepVal;
    }
}
