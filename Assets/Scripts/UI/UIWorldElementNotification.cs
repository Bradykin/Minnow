﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Util;
using TMPro;

public class UIWorldElementNotification : MonoBehaviour, IReset
{
    public TMP_Text m_notificationText;

    private float m_displayTimer;
    private float m_displayTimerLimit = 1.5f; //In seconds

    private float m_floatingSpeed = 0.01f;

    void Update()
    {
        m_displayTimer += Time.deltaTime;

        if (m_displayTimer >= m_displayTimerLimit)
        {
            Recycler.Recycle<UIWorldElementNotification>(this);
            return;
        }

        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + m_floatingSpeed, transform.localPosition.z);
    }

    public void Activate()
    {

    }

    public void Reset()
    {
        Recycler.Recycle<UIWorldElementNotification>(this);
    }

    public void Init(string message, Color color)
    {
        m_notificationText.text = message;
        m_notificationText.color = color;
    }
}
