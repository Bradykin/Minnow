using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISimpleTooltip : UITooltipBase
{
    public GameObject m_titleBox;
    public GameObject m_descBox;

    private SpriteRenderer m_titleRenderer;
    private SpriteRenderer m_descRenderer;

    public Text m_titleText;
    public Text m_descText;

    public void Init(string title, string desc)
    {
        m_titleRenderer = m_titleBox.GetComponent<SpriteRenderer>();
        m_descRenderer = m_descBox.GetComponent<SpriteRenderer>();

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
            m_titleRenderer.color = UIHelper.m_playerColor;
        }
        else
        {
            m_titleRenderer.color = UIHelper.m_enemyColor;
        }
    }

    public void Init(string title, string desc, bool isValid)
    {
        Init(title, desc);

        UIHelper.SetValidColor(m_titleRenderer, isValid);
    }

    void Update()
    {
        base.FrameUpdate();
    }
}
