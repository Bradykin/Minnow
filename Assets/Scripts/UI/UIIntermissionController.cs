using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

public class UIIntermissionController : Singleton<UIIntermissionController>
{
    public enum SelectorType
    {
        Action,
        Tech,
        Building
    }

    private SelectorType m_selectorType;

    public UIActionController m_actionOne;
    public UIActionController m_actionTwo;
    public UIActionController m_actionThree;

    private List<GameActionIntermission> m_intermissionActions;
    private List<GameBuildingIntermission> m_intermissionBuildings;

    private int m_index;

    void Start()
    {
        m_index = 0;

        m_intermissionActions = new List<GameActionIntermission>();
        m_intermissionBuildings = new List<GameBuildingIntermission>();

        m_intermissionActions.Add(new ContentResourcesIntermissionAction());
        m_intermissionActions.Add(new ContentCardIntermissionAction());
        m_intermissionActions.Add(new ContentRemovalIntermissionAction());
        //m_intermissionActions.Add(new ContentRelicIntermissionAction());

        m_intermissionBuildings.Add(new GameBuildingIntermission(new ContentForestLodgeBuilding()));
        m_intermissionBuildings.Add(new GameBuildingIntermission(new ContentFarmlandBuilding()));
        m_intermissionBuildings.Add(new GameBuildingIntermission(new ContentFrontierTownBuilding()));
        m_intermissionBuildings.Add(new GameBuildingIntermission(new ContentSmithyBuilding()));
        m_intermissionBuildings.Add(new GameBuildingIntermission(new ContentMineBuilding()));
        m_intermissionBuildings.Add(new GameBuildingIntermission(new ContentMagicSchoolBuilding()));
        m_intermissionBuildings.Add(new GameBuildingIntermission(new ContentTempleBuilding()));
        m_intermissionBuildings.Add(new GameBuildingIntermission(new ContentFortressBuilding()));
        m_intermissionBuildings.Add(new GameBuildingIntermission(new ContentWizardTowerBuilding()));

        UpdateActions();
    }

    public void SetSelectorType(SelectorType newType)
    {
        m_selectorType = newType;

        SetIndex(0);
    }

    public SelectorType GetSelectorType()
    {
        return m_selectorType;
    }

    public void SetIndex(int val)
    {
        m_index = val;

        UpdateActions();
    }

    public int GetIndex()
    {
        return m_index;
    }

    private void UpdateActions()
    {
        if (m_selectorType == SelectorType.Action)
        {
            if (m_intermissionActions.Count <= m_index * 3)
            {
                m_actionOne.gameObject.SetActive(false);
            }
            else
            {
                m_actionOne.gameObject.SetActive(true);
                m_actionOne.Init(m_intermissionActions[m_index * 3]);
            }

            if (m_intermissionActions.Count <= m_index * 3 + 1)
            {
                m_actionTwo.gameObject.SetActive(false);
            }
            else
            {
                m_actionTwo.gameObject.SetActive(true);
                m_actionTwo.Init(m_intermissionActions[m_index * 3 + 1]);
            }

            if (m_intermissionActions.Count <= m_index * 3 + 2)
            {
                m_actionThree.gameObject.SetActive(false);
            }
            else
            {
                m_actionThree.gameObject.SetActive(true);
                m_actionThree.Init(m_intermissionActions[m_index * 3 + 2]);
            }
        }
        else if (m_selectorType == SelectorType.Building)
        {
            if (m_intermissionBuildings.Count <= m_index * 3)
            {
                m_actionOne.gameObject.SetActive(false);
            }
            else
            {
                m_actionOne.gameObject.SetActive(true);
                m_actionOne.Init(m_intermissionBuildings[m_index * 3]);
            }

            if (m_intermissionBuildings.Count <= m_index * 3 + 1)
            {
                m_actionTwo.gameObject.SetActive(false);
            }
            else
            {
                m_actionTwo.gameObject.SetActive(true);
                m_actionTwo.Init(m_intermissionBuildings[m_index * 3 + 1]);
            }

            if (m_intermissionBuildings.Count <= m_index * 3 + 2)
            {
                m_actionThree.gameObject.SetActive(false);
            }
            else
            {
                m_actionThree.gameObject.SetActive(true);
                m_actionThree.Init(m_intermissionBuildings[m_index * 3 + 2]);
            }
        }
    }

    public bool CanIndexIncrease()
    {
        if (m_selectorType == SelectorType.Action)
        {
            if ((m_index + 1) * 3 < m_intermissionActions.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (m_selectorType == SelectorType.Building)
        {
            if ((m_index + 1) * 3 < m_intermissionBuildings.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }
}
