using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIActionController : MonoBehaviour
{
    public SpriteRenderer m_tintRenderer;
    public SpriteRenderer m_iconRenderer;

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
            UIHelper.SetSelectTintColor(m_tintRenderer, true);
        }
        else
        { 
            if (m_hovered)
            {
                UIHelper.SetValidTintColor(m_tintRenderer, m_actionController.CanAfford());
            }
            else
            {
                UIHelper.SetDefaultTintColor(m_tintRenderer);
            }
        }
    }

    public void Init(GameActionIntermission action)
    {
        m_actionController = new GameIntermissionActionController(action);

        m_iconRenderer.gameObject.transform.localPosition = new Vector3(3.26f, 0.05f, 0.0f);
        m_iconRenderer.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);

        InitImpl();
    }

    public void Init(GameTechIntermission tech)
    {
        m_actionController = new GameIntermissionActionController(tech);

        m_iconRenderer.gameObject.transform.localPosition = new Vector3(3.26f, 0.05f, 0.0f);
        m_iconRenderer.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);

        InitImpl();
    }

    public void Init(GameBuildingIntermission building)
    {
        m_actionController = new GameIntermissionActionController(building);

        m_iconRenderer.gameObject.transform.localPosition = new Vector3(3.26f, 0.68f, 0.0f);
        m_iconRenderer.gameObject.transform.localScale = new Vector3(0.6f, 0.6f, 1.0f);

        InitImpl();
    }

    private void InitImpl()
    {
        m_iconRenderer.sprite = m_actionController.GetIcon();

        m_titleText.text = m_actionController.GetName();
        m_descText.text = m_actionController.GetDesc();
        if (m_actionController.HasAction())
        {
            m_actionCostText.text = "Actions: " + m_actionController.GetActionCost();
        }
        else if (m_actionController.HasBuilding())
        {
            m_actionCostText.text = "Max Health: " + m_actionController.m_building.m_building.m_maxHealth;
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
