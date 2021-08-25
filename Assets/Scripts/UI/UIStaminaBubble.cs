using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStaminaBubble : MonoBehaviour
{
    public Image m_image;
    public Image m_borderImage;

    public Sprite m_emptyBorder;
    public Sprite m_emptyBubble;

    public Sprite m_playerBorder;
    public Sprite m_playerBubble;
    
    public Sprite m_enemyBorder;
    public Sprite m_enemyBubble;

    public void Init(bool active, Team team)
    {
        Color color = UIHelper.GetValidColorAltByTeam(active, team);

        if (color == UIHelper.m_validAltEnemy)
        {
            m_image.sprite = m_enemyBubble;
            m_borderImage.sprite = m_enemyBorder;
        }
        else if (color == UIHelper.m_validAltPlayer)
        {
            m_image.sprite = m_playerBubble;
            m_borderImage.sprite = m_playerBorder;
        }
        else if (color == UIHelper.m_invalidAlt)
        {
            m_image.sprite = m_emptyBubble;
            m_borderImage.sprite = m_emptyBorder;
        }
    }
}
