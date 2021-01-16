using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

public class UIIntermissionController : Singleton<UIIntermissionController>
{
    public enum SelectionOptions
    {
        Unit,
        Spell,
        Action
    }

    public List<SelectionOptions> m_selectionStack = new List<SelectionOptions>();

    public UIBuildingController m_buildingOne;
    public UIBuildingController m_buildingTwo;
    public UIBuildingController m_buildingThree;

    private List<GameBuildingIntermission> m_intermissionBuildings;

    private int m_index;

    void Start()
    {
        m_index = 0;

        m_intermissionBuildings = new List<GameBuildingIntermission>();

        bool isOnLakeside = GameHelper.IsCurrentMapLakeside();

        m_intermissionBuildings.Add(new GameBuildingIntermission(new ContentForestLodgeBuilding()));
        m_intermissionBuildings.Add(new GameBuildingIntermission(new ContentFortressBuilding()));
        m_intermissionBuildings.Add(new GameBuildingIntermission(new ContentRingOfProtectionBuilding()));

        m_intermissionBuildings.Add(new GameBuildingIntermission(new ContentMagicSchoolBuilding()));
        m_intermissionBuildings.Add(new GameBuildingIntermission(new ContentWizardTowerBuilding()));
        m_intermissionBuildings.Add(new GameBuildingIntermission(new ContentManaLocusBuilding()));

        m_intermissionBuildings.Add(new GameBuildingIntermission(new ContentHillFortBuilding()));
        m_intermissionBuildings.Add(new GameBuildingIntermission(new ContentMountainGatewayBuilding()));
        m_intermissionBuildings.Add(new GameBuildingIntermission(new ContentFrontierTownBuilding()));

        m_intermissionBuildings.Add(new GameBuildingIntermission(new ContentFarmlandBuilding()));
        m_intermissionBuildings.Add(new GameBuildingIntermission(new ContentSmithyBuilding()));
        m_intermissionBuildings.Add(new GameBuildingIntermission(new ContentTempleBuilding()));


        UpdateActions();
    }

    public void StartSelectionMenus()
    {
        m_selectionStack.Add(SelectionOptions.Unit);
        m_selectionStack.Add(SelectionOptions.Spell);
        m_selectionStack.Add(SelectionOptions.Action);

        TriggerNextSelection();
    }

    public void TriggerNextSelection()
    {
        if (m_selectionStack.Count == 0)
        {
            return;
        }

        if (m_selectionStack[0] == SelectionOptions.Unit)
        {
            UIHelper.TriggerUnitCardSelection();
        }
        else if (m_selectionStack[0] == SelectionOptions.Spell)
        {
            UIHelper.TriggerSpellCardSelection();
        }
        else if (m_selectionStack[0] == SelectionOptions.Action)
        {
            UIHelper.TriggerActionSelection();
        }

        m_selectionStack.RemoveAt(0);
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
        if (m_intermissionBuildings.Count <= m_index * 3)
        {
            m_buildingOne.gameObject.SetActive(false);
        }
        else
        {
            m_buildingOne.gameObject.SetActive(true);
            m_buildingOne.Init(m_intermissionBuildings[m_index * 3]);
        }

        if (m_intermissionBuildings.Count <= m_index * 3 + 1)
        {
            m_buildingTwo.gameObject.SetActive(false);
        }
        else
        {
            m_buildingTwo.gameObject.SetActive(true);
            m_buildingTwo.Init(m_intermissionBuildings[m_index * 3 + 1]);
        }

        if (m_intermissionBuildings.Count <= m_index * 3 + 2)
        {
            m_buildingThree.gameObject.SetActive(false);
        }
        else
        {
            m_buildingThree.gameObject.SetActive(true);
            m_buildingThree.Init(m_intermissionBuildings[m_index * 3 + 2]);
        }
    }

    public bool CanIndexIncrease()
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
}
