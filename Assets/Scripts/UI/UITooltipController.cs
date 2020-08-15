using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITooltipController : MonoBehaviour
{
    public static UITooltipController m_instance;

    public Text m_titleText;
    public Text m_descText;
    public SpriteRenderer m_titleBackground;
    public SpriteRenderer m_descBackground;

    void Start()
    {
        if (!m_instance)
        {
            m_instance = this;
        }

        gameObject.SetActive(false);
    }

    void Update()
    {
        
    }
}
