using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIActionController : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
    , IPointerClickHandler
{
    public Image m_tintImage;
    public Image m_iconBuildingImage;
    public Image m_iconActionImage;
    public Image m_terrainImage;

    public TMP_Text m_titleText;
    public TMP_Text m_descText;

    public TMP_Text m_actionCostText;
    public TMP_Text m_goldCostText;

    public GameObject m_resourcesCostHolder;

    public GameIntermissionActionController m_actionController;

    private bool m_hovered;

    void Update()
    {
        if (m_actionController.HasBuilding() && Globals.m_selectedIntermissionBuilding == m_actionController.m_building)
        {
            m_tintImage.color = UIHelper.GetSelectTintColor(true);
        }
        else
        { 
            if (m_hovered)
            {
                m_tintImage.color = UIHelper.GetValidTintColor(m_actionController.CanAfford());
            }
            else
            {
                m_tintImage.color = UIHelper.GetDefaultTintColor();
            }
        }
    }

    public void Init(GameActionIntermission action)
    {
        m_actionController = new GameIntermissionActionController(action);

        m_iconBuildingImage.gameObject.SetActive(false);
        m_iconActionImage.gameObject.SetActive(true);

        InitImpl();
    }

    public void Init(GameBuildingIntermission building)
    {
        m_actionController = new GameIntermissionActionController(building);

        m_iconBuildingImage.gameObject.SetActive(true);
        m_iconActionImage.gameObject.SetActive(false);

        InitImpl();
    }

    private void InitImpl()
    {
        m_iconBuildingImage.sprite = m_actionController.GetIcon();
        m_iconActionImage.sprite = m_actionController.GetIcon();

        m_titleText.text = m_actionController.GetName();
        m_descText.text = m_actionController.GetDesc();
        if (m_actionController.HasAction())
        {
            m_actionCostText.text = "Actions: " + m_actionController.GetActionCost();
            m_terrainImage.gameObject.SetActive(false);
        }
        else if (m_actionController.HasBuilding())
        {
            GameBuildingBase building = m_actionController.m_building.m_building;

            m_actionCostText.text = "Max Health: " + building.m_maxHealth;
            m_terrainImage.gameObject.SetActive(true);

            ContentForestTerrain forestTest = new ContentForestTerrain();
            ContentMountainTerrain mountainTest = new ContentMountainTerrain();
            ContentDirtPlainsTerrain dirtTest = new ContentDirtPlainsTerrain();
            ContentWaterTerrain waterTest = new ContentWaterTerrain();
            ContentHillsTerrain hillTest = new ContentHillsTerrain();

            if (building.IsValidTerrainToPlace(forestTest, null))
            {
                m_terrainImage.sprite = forestTest.m_icon;
            }
            else if (building.IsValidTerrainToPlace(mountainTest, null))
            {
                m_terrainImage.sprite = mountainTest.m_icon;
            }
            else if (building.IsValidTerrainToPlace(dirtTest, null))
            {
                m_terrainImage.sprite = dirtTest.m_icon;
            }
            else if (building.IsValidTerrainToPlace(waterTest, null))
            {
                m_terrainImage.sprite = waterTest.m_icon;
            }
            else if (building.IsValidTerrainToPlace(hillTest, null))
            {
                m_terrainImage.sprite = hillTest.m_icon;
            }
        }

        GameWallet costWallet = m_actionController.GetWallet();

        if (costWallet == null)
        {
            m_resourcesCostHolder.SetActive(false);
        }
        else
        {
            m_resourcesCostHolder.SetActive(true);

            m_goldCostText.text = "" + costWallet.m_gold;
        }
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

        m_actionController.Activate();
    }
}
