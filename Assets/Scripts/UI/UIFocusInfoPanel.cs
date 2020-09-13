﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFocusInfoPanel : WorldElementBase
{
    public Text m_titleText;
    public Text m_descText;
    public GameObject m_holder;

    public SpriteRenderer m_tintRenderer;

    private bool m_shouldShow;

    void Update()
    {
        m_titleText.text = "";
        m_descText.text = "";

        if (Globals.m_selectedCard != null)
        {
            UpdateFocusData(Globals.m_selectedCard);
        } 
        else if (Globals.m_selectedEnemy != null)
        {
            UpdateFocusData(Globals.m_selectedEnemy);
        }
        else if (Globals.m_selectedEntity != null)
        {
            UpdateFocusData(Globals.m_selectedEntity);
        }
        else if (Globals.m_selectedTile != null)
        {
            UpdateFocusData(Globals.m_selectedTile);
        }
        else
        {
            m_shouldShow = false;
        }

        m_holder.SetActive(m_shouldShow);
    }

    private void UpdateFocusData(UICard cardData)
    {
        if (cardData.m_card is GameCardEntityBase)
        {
            m_shouldShow = true;
        }
        else
        {
            m_shouldShow = false;
            return;
        }

        GameCardEntityBase entityCard = (GameCardEntityBase)(cardData.m_card);

        m_titleText.text = entityCard.GetName();
        List<GameKeywordBase> keywords = entityCard.GetEntity().GetKeywordHolderForRead().m_keywords;
        for (int i = 0; i < keywords.Count; i++)
        {
            m_descText.text += keywords[i].GetFocusInfoText();
        }
    }

    private void UpdateFocusData(UIEntity entityData)
    {
        m_shouldShow = true;

        m_titleText.text = entityData.GetEntity().GetName();
        List<GameKeywordBase> keywords = entityData.GetEntity().GetKeywordHolderForRead().m_keywords;
        for (int i = 0; i < keywords.Count; i++)
        {
            m_descText.text += keywords[i].GetFocusInfoText();
        }
    }

    private void UpdateFocusData(WorldTile worldTile)
    {
        m_shouldShow = true;

        m_titleText.text = worldTile.GetGameTile().GetName();
        m_descText.text = "Protection: " + worldTile.GetGameTile().GetDamageReduction(null);
        if (!worldTile.GetGameTile().IsPassable(null, false))
        {
            m_descText.text += "\n\nNot Passable" + "\n\n";
        }
        else
        {
            m_descText.text += "\n\nAP Cost: " + worldTile.GetGameTile().GetCostToPass(null) + "\n\n";
        }

        m_descText.text += worldTile.GetGameTile().GetFocusPanelText();
    }

    void OnMouseOver()
    {
        UIHelper.SetValidTintColor(m_tintRenderer, true);
        Globals.m_canScroll = false;
    }

    void OnMouseExit()
    {
        UIHelper.SetDefaultTintColor(m_tintRenderer);
        Globals.m_canScroll = true;
    }

    void OnMouseDown()
    {
        //Focus on the element
    }

    public override void HandleTooltip()
    {
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Info", "This will give you more info about the things you have selected."));
    }
}
