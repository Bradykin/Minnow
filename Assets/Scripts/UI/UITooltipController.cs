using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITooltipController : Singleton<UITooltipController>
{
    public Text m_titleText;
    public Text m_descText;
    public SpriteRenderer m_titleBackground;
    public SpriteRenderer m_descBackground;

    void Start()
    {
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
