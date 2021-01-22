using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Util;
using TMPro;

public class UIFocusInfoPanel : UIElementBase
{
    public TMP_Text m_titleText;
    public TMP_Text m_descText;
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
        else if (Globals.m_selectedEnemy != null && Globals.m_selectedEnemy.GetUnit() != null)
        {
            UpdateFocusData(Globals.m_selectedEnemy);
        }
        else if (Globals.m_selectedUnit != null && Globals.m_selectedUnit.GetUnit() != null)
        {
            UpdateFocusData(Globals.m_selectedUnit);
        }
        else if (Globals.m_selectedTile != null)
        {
            UpdateFocusData(Globals.m_selectedTile);
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
            if (unitCard.GetUnit().GetKeywordHolderForRead().GetNumKeywords() == 0)
            {
                m_shouldShow = false;
                return;
            }

            m_shouldShow = true;

            m_titleText.text = unitCard.GetName();
            IReadOnlyList<GameKeywordBase> keywords = unitCard.GetUnit().GetKeywordHolderForRead().GetKeywordsForRead();
            for (int i = 0; i < keywords.Count; i++)
            {
                m_descText.text += "<b>" + keywords[i].GetName() + "</b>: " + keywords[i].GetFocusInfoText() + "\n\n";
            }
        }
        else if (cardData.m_card is GameCardSpellBase)
        {
            GameCardSpellBase spellCard = (GameCardSpellBase)(cardData.m_card);

            //Don't show this if there are no keywords, and it is not an exile or x spell
            if (spellCard.GetKeywordHolderForRead().GetNumKeywords() == 0 && !spellCard.m_shouldExile && !spellCard.IsXSpell())
            {
                m_shouldShow = false;
                return;
            }

            m_titleText.text = spellCard.GetName();

            if (spellCard.m_shouldExile)
            {
                m_descText.text += "<b>Exile Spells</b>: This spell is removed from your deck after being cast.  It is returned to your deck in the next wave.\n\n";
            }

            if (spellCard.IsXSpell())
            {
                m_descText.text += "<b>X Spells</b>: This spell consumes all available energy, and then sets the value of X on all effects of the card equal to the amount of energy used.\n\n";
            }

            IReadOnlyList<GameKeywordBase> keywords = spellCard.GetKeywordHolderForRead().GetKeywordsForRead();
            for (int i = 0; i < keywords.Count; i++)
            {
                m_descText.text += "<b>" + keywords[i].GetName() + "</b>: " + keywords[i].GetFocusInfoText() + "\n\n";
            }

            m_shouldShow = true;
        }
    }

    private void UpdateFocusData(WorldUnit unitData)
    {
        m_shouldShow = true;

        //Don't show this if there are no keywords.
        if (unitData.GetUnit().GetKeywordHolderForRead().GetNumKeywords() == 0)
        {
            m_shouldShow = false;
            return;
        }

        m_titleText.text = unitData.GetUnit().GetName();
        IReadOnlyList<GameKeywordBase> keywords = unitData.GetUnit().GetKeywordHolderForRead().GetKeywordsForRead();
        for (int i = 0; i < keywords.Count; i++)
        {
            m_descText.text += "<b>" + keywords[i].GetName() + "</b>: " + keywords[i].GetFocusInfoText() + "\n\n";
        }
    }

    private void UpdateFocusData(WorldTile worldTile)
    {
        m_shouldShow = true;

        GameTile gameTile = worldTile.GetGameTile();

        if (gameTile == null)
        {
            return;
        }

        m_titleText.text = gameTile.GetName();

        if (gameTile.GetTerrain().GetCoverType() == GameTerrainBase.CoverType.Cover ||
            gameTile.HasBuilding())
        {
            m_descText.text = "Cover: " + Constants.CoverProtectionPercent + "% reduced damage.\n";
        }

        if (!gameTile.IsPassable(null, false))
        {
            m_descText.text += "\n\nNot Passable" + "\n";
        }
        else
        {
            if (gameTile.GetTerrain().GetMovementType() == GameTerrainBase.TerrainMovementType.Difficult)
            {
                m_descText.text += "\nMovement Cost: Difficult (2)";
            }
            else if (gameTile.GetTerrain().GetMovementType() == GameTerrainBase.TerrainMovementType.Extreme)
            {
                m_descText.text += "\nMovement Cost: Extreme (3)";
            }
        }

        m_descText.text += gameTile.GetTerrain().GetDesc();
        
        if (gameTile.HasBuilding())
        {
            m_descText.text += "\n\n" + gameTile.GetBuilding().GetDesc();
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
