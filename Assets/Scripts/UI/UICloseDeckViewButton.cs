﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UICloseDeckViewButton : UIElementBase
    , IPointerClickHandler
{
    public GameObject m_holder;

    void Update()
    {
        //m_holder.SetActive(UIDeckViewController.Instance.m_viewType == UIDeckViewController.DeckViewType.View);
    }

    public override void HandleTooltip()
    {
        //Left as stub.
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!m_holder.activeSelf)
        {
            return;
        }

        AudioSFXController.Instance.PlaySFX(AudioHelper.UIClick);

        Globals.m_inDeckView = false;

        UIDeckViewController.Instance.CloseDeckView();
    }
}
