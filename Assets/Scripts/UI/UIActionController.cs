using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIActionController : MonoBehaviour
{
    public Image m_tintImage;
    public Image m_iconImage;
    public Image m_terrainImage;

    public Text m_titleText;
    public Text m_descText;

    public Text m_actionCostText;
    public Text m_goldCostText;

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

        m_iconImage.gameObject.transform.localPosition = new Vector3(3.26f, 0.05f, 0.0f);
        m_iconImage.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);

        InitImpl();
    }

    public void Init(GameBuildingIntermission building)
    {
        m_actionController = new GameIntermissionActionController(building);

        m_iconImage.gameObject.transform.localPosition = new Vector3(3.26f, 0.68f, 0.0f);
        m_iconImage.gameObject.transform.localScale = new Vector3(0.6f, 0.6f, 1.0f);

        InitImpl();
    }

    private void InitImpl()
    {
        m_iconImage.sprite = m_actionController.GetIcon();

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

            if (building.IsValidTerrainToPlace(forestTest))
            {
                m_terrainImage.sprite = forestTest.m_icon;
            }
            else if (building.IsValidTerrainToPlace(mountainTest))
            {
                m_terrainImage.sprite = mountainTest.m_icon;
            }
            else if (building.IsValidTerrainToPlace(dirtTest))
            {
                m_terrainImage.sprite = dirtTest.m_icon;
            }
            else if (building.IsValidTerrainToPlace(waterTest))
            {
                m_terrainImage.sprite = waterTest.m_icon;
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

    void OnMouseDown()
    {
        if (!Globals.m_canSelect)
        {
            return;
        }

        m_actionController.Activate();
    }

    void OnMouseOver()
    {
        m_hovered = true;
    }

    void OnMouseExit()
    {
        m_hovered = false;
    }
}
