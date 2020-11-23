using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISimpleTooltip : UITooltipBase
{
    public GameObject m_titleBox;
    public GameObject m_descBox;

    private Image m_titleImage;
    private Image m_descImage;

    public TMP_Text m_titleText;
    public TMP_Text m_descText;

    public void Init(string title, string desc)
    {
        m_titleImage = m_titleBox.GetComponent<Image>();
        m_descImage = m_descBox.GetComponent<Image>();

        m_titleText.text = title;

        m_descBox.SetActive(true);
        m_descText.gameObject.SetActive(true);
        m_descText.text = desc;

        m_height = 120.0f;

        m_widthLeft = 3f;
        m_widthRight = 3f;
    }

    public void Init(string title, string desc, Team team)
    {
        Init(title, desc);

        if (team == Team.Player)
        {
            m_titleImage.color = UIHelper.m_playerColor;
        }
        else
        {
            m_titleImage.color = UIHelper.m_enemyColor;
        }
    }

    public void Init(string title, string desc, bool isValid)
    {
        Init(title, desc);

        m_titleImage.color = UIHelper.GetValidColor(isValid);
    }

    void Update()
    {
        base.FrameUpdate();
    }
}
