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
        Vector3 pos = Input.mousePosition;
        pos.z = transform.position.z - Camera.main.transform.position.z;

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(pos);
        worldPoint.x = worldPoint.x + 1.3f;
        worldPoint.y = worldPoint.y - 0.3f;
        transform.position = worldPoint;
    }
}
