using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISimpleTooltip : UITooltipBase
{
    public GameObject m_titleBox;
    public GameObject m_descBox;

    public Text m_titleText;
    public Text m_descText;

    public void Init(string title, string desc)
    {
        m_titleText.text = title;

        if (desc == null)
        {
            m_descBox.SetActive(false);
            m_descText.gameObject.SetActive(false);

            m_height = 1.0f;
        }
        else
        {
            m_descBox.SetActive(true);
            m_descText.gameObject.SetActive(true);
            m_descText.text = desc;

            m_height = 4.0f;
        }
    }

    public void Init(string title, string desc, Team team)
    {
        Init(title, desc);

        if (team == Team.Player)
        {
            m_titleBox.GetComponent<SpriteRenderer>().color = UIHelper.m_playerColor;
        }
        else
        {
            m_titleBox.GetComponent<SpriteRenderer>().color = UIHelper.m_enemyColor;
        }
    }

    void Update()
    {
        base.FrameUpdate();
    }
}
