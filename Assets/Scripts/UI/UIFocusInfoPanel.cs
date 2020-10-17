using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Util;

public class UIFocusInfoPanel : UIElementBase
{
    public Text m_titleText;
    public Text m_descText;
    public GameObject m_holder;

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
        else if (Globals.m_selectedUnit != null)
        {
            UpdateFocusData(Globals.m_selectedUnit);
        }
        else if (Globals.m_selectedTile != null)
        {
            UpdateFocusData(Globals.m_selectedTile);
        }
        else if (Globals.m_hoveredTile != null)
        {
            UpdateFocusData(Globals.m_hoveredTile);
        }
        else if (Globals.m_hoveredCard != null)
        {
            UpdateFocusData(Globals.m_hoveredCard);
        }
        else
        {
            m_shouldShow = false;
        }

        m_holder.SetActive(m_shouldShow);
    }

    private void UpdateFocusData(UICard cardData)
    {
        if (cardData.m_card is GameUnitCard)
        {
            GameUnitCard unitCard = (GameUnitCard)(cardData.m_card);

            //Don't show this if there are no keywords.
            if (unitCard.GetUnit().GetKeywordHolderForRead().m_keywords.Count == 0)
            {
                m_shouldShow = false;
                return;
            }

            m_shouldShow = true;

            m_titleText.text = unitCard.GetName();
            List<GameKeywordBase> keywords = unitCard.GetUnit().GetKeywordHolderForRead().m_keywords;
            for (int i = 0; i < keywords.Count; i++)
            {
                m_descText.text += "<b>" + keywords[i].m_name + "</b>: " + keywords[i].GetFocusInfoText() + "\n\n";
            }
        }
        else if (cardData.m_card is GameCardSpellBase)
        {
            GameCardSpellBase spellCard = (GameCardSpellBase)(cardData.m_card);

            //Don't show this if there are no keywords.
            if (spellCard.GetKeywordHolderForRead().m_keywords.Count == 0)
            {
                m_shouldShow = false;
                return;
            }

            m_titleText.text = spellCard.GetName();

            if (spellCard.m_shouldExile)
            {
                m_descText.text += "Exile spells are removed from your deck after being cast.  They are returned for the next wave.\n\n";
            }

            List<GameKeywordBase> keywords = spellCard.GetKeywordHolderForRead().m_keywords;
            for (int i = 0; i < keywords.Count; i++)
            {
                m_descText.text += "<b>" + keywords[i].m_name + "</b>: " + keywords[i].GetFocusInfoText() + "\n\n";
            }

            m_shouldShow = true;
        }
    }

    private void UpdateFocusData(WorldUnit unitData)
    {
        m_shouldShow = true;

        //Don't show this if there are no keywords.
        if (unitData.GetUnit().GetKeywordHolderForRead().m_keywords.Count == 0)
        {
            m_shouldShow = false;
            return;
        }

        m_titleText.text = unitData.GetUnit().GetName();
        List<GameKeywordBase> keywords = unitData.GetUnit().GetKeywordHolderForRead().m_keywords;
        for (int i = 0; i < keywords.Count; i++)
        {
            m_descText.text += "<b>" + keywords[i].m_name + "</b>: " + keywords[i].GetFocusInfoText() + "\n\n";
        }
    }

    private void UpdateFocusData(WorldTile worldTile)
    {
        m_shouldShow = true;

        m_titleText.text = worldTile.GetGameTile().GetName();
        m_descText.text = "Protection: " + worldTile.GetGameTile().GetDamageReduction(null);
        if (!worldTile.GetGameTile().IsPassable(null, false))
        {
            m_descText.text += "\n\nNot Passable" + "\n";
        }
        else
        {
            m_descText.text += "\n\nStamina Cost: " + worldTile.GetGameTile().GetCostToPass(null) + "\n\n";
        }

        m_descText.text += worldTile.GetGameTile().GetFocusPanelText() + "\n";

        if (worldTile.GetGameTile().GetTerrain().IsEventTerrain())
        {
            m_descText.text += "An unknown event! Moving a unit here may do something good...\n";
        }
    }

    public override void HandleTooltip()
    {
        if (!m_holder.activeSelf)
        {
            return;
        }

        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Info", "This will give you more info about the things you have selected."));
    }
}
