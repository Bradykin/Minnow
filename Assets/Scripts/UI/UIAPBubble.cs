using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAPBubble : MonoBehaviour
{
    public Image m_image;

    public void Init(bool active, Team team)
    {
        m_image.color = UIHelper.GetValidColorAltByTeam(active, team);
    }
}
