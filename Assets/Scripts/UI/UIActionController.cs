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
    public Text m_magicCostText;
    public Text m_brickCostText;

    public GameObject m_resourcesCostHolder;

    public GameIntermissionActionController m_actionController;

    public void Init(GameActionIntermission action)
    {
        m_actionController = new GameIntermissionActionController(action);

        InitImpl();
    }

    public void Init(GameTechIntermission tech)
    {
        m_actionController = new GameIntermissionActionController(tech);

        InitImpl();
    }

    public void Init(GameBuildingIntermission building)
    {
        m_actionController = new GameIntermissionActionController(building);

        InitImpl();
    }

    private void InitImpl()
    {
        m_iconRenderer.sprite = m_actionController.GetIcon();

        m_titleText.text = m_actionController.GetName();
        m_descText.text = m_actionController.GetDesc();
        m_actionCostText.text = "Actions: " + m_actionController.GetActionCost();

        GameWallet costWallet = m_actionController.GetWallet();

        if (costWallet == null)
        {
            m_resourcesCostHolder.SetActive(false);
        }
        else
        {
            m_resourcesCostHolder.SetActive(true);

            m_goldCostText.text = "" + costWallet.m_gold;
            m_magicCostText.text = "" + costWallet.m_magic;
            m_brickCostText.text = "" + costWallet.m_bricks;
        }
    }

    void OnMouseDown()
    {
        m_actionController.Activate();
    }

    void OnMouseOver()
    {
        UIHelper.SetValidTintColor(m_tintRenderer, true);
    }

    void OnMouseExit()
    {
        UIHelper.SetDefaultTintColor(m_tintRenderer);
    }
}
