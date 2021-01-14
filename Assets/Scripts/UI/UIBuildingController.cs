using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIBuildingController : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
    , IPointerClickHandler
{
    public Image m_tintImage;
    public Image m_iconBuildingImage;
    public Image m_terrainImage;

    public TMP_Text m_titleText;
    public TMP_Text m_descText;

    public TMP_Text m_goldCostText;
    public TMP_Text m_healthText;

    public GameBuildingIntermission m_building;

    private bool m_hovered;

    void Update()
    {
        if (Globals.m_selectedIntermissionBuilding == m_building)
        {
            m_tintImage.color = UIHelper.GetSelectTintColor(true);
        }
        else
        { 
            if (m_hovered)
            {
                m_tintImage.color = UIHelper.GetValidTintColor(m_building.CanAfford());
            }
            else
            {
                m_tintImage.color = UIHelper.GetDefaultTintColor();
            }
        }
    }

    public void Init(GameBuildingIntermission building)
    {
        m_building = building;

        InitImpl();
    }

    private void InitImpl()
    {
        m_iconBuildingImage.sprite = m_building.m_building.GetIcon();

        m_titleText.text = m_building.m_building.GetName();
        m_descText.text = m_building.m_building.GetDesc();

        m_healthText.text = "Max Health: " + m_building.m_building.m_maxHealth;

        ContentForestTerrain forestTest = new ContentForestTerrain();
        ContentMountainTerrain mountainTest = new ContentMountainTerrain();
        ContentDirtPlainsTerrain dirtTest = new ContentDirtPlainsTerrain();
        ContentWaterTerrain waterTest = new ContentWaterTerrain();
        ContentHillsTerrain hillTest = new ContentHillsTerrain();

        if (m_building.m_building.IsValidTerrainToPlace(forestTest, null))
        {
            m_terrainImage.sprite = forestTest.m_icon;
        }
        else if (m_building.m_building.IsValidTerrainToPlace(mountainTest, null))
        {
            m_terrainImage.sprite = mountainTest.m_icon;
        }
        else if (m_building.m_building.IsValidTerrainToPlace(dirtTest, null))
        {
            m_terrainImage.sprite = dirtTest.m_icon;
        }
        else if (m_building.m_building.IsValidTerrainToPlace(waterTest, null))
        {
            m_terrainImage.sprite = waterTest.m_icon;
        }
        else if (m_building.m_building.IsValidTerrainToPlace(hillTest, null))
        {
            m_terrainImage.sprite = hillTest.m_icon;
        }

        m_goldCostText.text = "" + m_building.m_cost.m_gold;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_hovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_hovered = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!Globals.m_canSelect)
        {
            return;
        }

        if (!m_building.CanAfford())
        {
            UIHelper.CreateMousePointerNotification("Not enough gold.", false);
            AudioHelper.PlaySFX(AudioHelper.UIError);
            return;
        }

        if (Globals.m_selectedIntermissionBuilding == m_building)
        {
            Globals.m_selectedIntermissionBuilding = null;
        }
        else
        {
            Globals.m_selectedIntermissionBuilding = m_building;
        }
    }
}
